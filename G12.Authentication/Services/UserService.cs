using G12.Authentication.Entities;
using G12.Authentication.Enums;
using G12.Authentication.Interfaces;
using G12.Authentication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace G12.Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationDbContext _dbContext;
        public UserService(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Thêm mới người dùng
        /// nvkhai  16.11.2020
        /// </summary>
        /// <param name="userRegisterReq">Thông tin đăng ký user</param>
        /// <returns></returns>
        public async Task<AppResponse> CreateUser(UserRegisterReq userRegisterReq, bool IsAdminGroup = false)
        {
            var user = new User();
            if (string.IsNullOrEmpty(userRegisterReq.Email) || string.IsNullOrEmpty(userRegisterReq.UserName) || string.IsNullOrEmpty(userRegisterReq.Password) 
                || userRegisterReq.Password.Length <= 8 || userRegisterReq.UserName.Length <= 8 || string.IsNullOrEmpty(userRegisterReq.GroupCode))
            {
                return new AppResponse(ResponseCode.ValidationError, "Tham số đầu vào không hợp lệ.");
            }

            if (_dbContext.Set<User>().Any(x => x.UserName == userRegisterReq.UserName))
            {
                return new AppResponse(ResponseCode.ValidationError, "UserName đã tồn tại trong hệ thống.");
            }

            var group = _dbContext.Set<GroupInfor>().Where(g => g.Code == userRegisterReq.GroupCode).FirstOrDefault();
            if (group == null)
            {
                return new AppResponse(ResponseCode.ValidationError, "GroupCode không tồn tại.");
            }

            var role = new Role
            {
                RoleType = IsAdminGroup ? RoleTypeEnums.GROUP_ADMIN : RoleTypeEnums.GROUP_USER,
                RoleGroup = group.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = RoleTypeNameEnums.SYS_ADMIN
            };

            CreatePasswordHash(userRegisterReq.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Role.Add(role);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UserName = userRegisterReq.UserName;
            user.Name = userRegisterReq.Name;
            user.Email = userRegisterReq.Email;
            user.Class = userRegisterReq.Class;
            user.DateOfBirth = userRegisterReq.DateOfBirth;
            user.Address = userRegisterReq.Address;
            user.PhoneNumber = userRegisterReq.PhoneNumber;
            user.CreatedDate = DateTime.Now;
            user.Active = true;
            user.CreatedBy = RoleTypeNameEnums.SYS_ADMIN;

            await _dbContext.Set<User>().AddAsync(user);
             var resultAddUser = await _dbContext.SaveChangesAsync();
            if (resultAddUser != 2)
            {
                return new AppResponse(ResponseCode.Exception, "Không thể thêm người dùng vào MainApp.", null);
            }
            var userClient = await MapUserToUserClient(user);
            var userContentReqToSubApp = JsonConvert.SerializeObject(userClient);

            try
            {
                //TODO: Register user for App
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(userContentReqToSubApp, Encoding.UTF8, "application/json");
                    var urlInstall = group.UrlInstall;
                    var resultRegisterUserInSubApp = await client.PostAsync(urlInstall, httpContent);
                    if (!resultRegisterUserInSubApp.IsSuccessStatusCode)
                    {
                        return new AppResponse(ResponseCode.IntergratesFail, "Không thể thêm người dùng vào SubApp.", null);
                    }
                }
            }
            catch (Exception)
            {
                _dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();
                return new AppResponse(ResponseCode.IntergratesFail, "Không thể kết nối tới SubApp.", null);
            }
            return new AppResponse(ResponseCode.Success, "", userClient);
        }

        /// <summary>
        /// Lấy về người dùng theo Id
        /// nvkhai 16.11.2020
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User GetUserById(Guid Id)
        {
            return _dbContext.User.Find(Id) ?? null;
        }

        /// <summary>
        /// Lấy về một chuỗi tổng hợp các quyền hạn của người dùng
        /// nvkhai 17.11.2020
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<string> GetListUserRolesByIdAsync(Guid Id)
        {
            var listRoles = new StringBuilder("|");
            var query = from role in _dbContext.Role.Where(r => r.UserId == Id)
                        join roleType in _dbContext.RoleType
                            on role.RoleType equals roleType.Id
                        join groupInfor in _dbContext.GroupInfor
                            on role.RoleGroup equals groupInfor.Id
                        select new { _role = roleType.Code, _group = groupInfor.Code };

            if (query == null || !query.Any())
            {
                throw new NullReferenceException("Không có Role và Group người dùng.");
            }

            foreach (var item in query)
            {
                listRoles.Append($"{item._role}/{item._group}|");
            }
            return await Task.FromResult(listRoles.ToString());
        }

        /// <summary>
        /// Tạo PasswordHash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// xác nhận Password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Đăng nhập basic cho User
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="userLoginReq"></param>
        /// <returns></returns>
        public async Task<User> Authenticate(UserLoginReq userLoginReq)
        {
            var user = _dbContext.Set<User>().SingleOrDefault(x => x.UserName == userLoginReq.UserName);
            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(userLoginReq.PassWord, user.PasswordHash, user.PasswordSalt))
                return null;
            // authentication successful
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Chuyển User sang UserClient mục đích trả về cho người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserClientRes> MapUserToUserClient(User user)
        {
            var ListRoles = await GetListUserRolesByIdAsync(user.Id);
            var userClient = new UserClientRes
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Class = user.Class,
                Active = user.Active,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate,
                CreatedBy = user.CreatedBy,
                Modifiedby = user.Modifiedby,
                ListRoles = ListRoles,
            };
            return userClient;
        }
    }
}
