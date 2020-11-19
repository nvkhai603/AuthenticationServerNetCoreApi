using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using G12.Authentication.Entities;
using G12.Authentication.Models;
using G12.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using G12.Authentication.Enums;
using G12.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace G12.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntergratesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IConfiguration _configuration;

        public IntergratesController(IUserService userService, IGroupService groupService, IConfiguration configuration)
        {
            _userService = userService;
            _groupService = groupService;
            _configuration = configuration;
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// nvkhai 17.11.2020
        /// </summary>
        /// <param name="userRegisterReq"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<AppResponse>> RegisterUser(UserRegisterReq userRegisterReq)
        {
            var result = await _userService.CreateUser(userRegisterReq);
            return Ok(result);
        }

        /// <summary>
        /// Đăng nhập basic
        /// nvkhai 17.11.2020
        /// </summary>
        /// <param name="userLoginReq"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AppResponse>> LoginUser(UserLoginReq userLoginReq)
        {
            var user = await _userService.Authenticate(userLoginReq);

            if (user == null)
                return BadRequest(new AppResponse(ResponseCode.ValidationError, "Tài khoản hoặc mật khẩu không chính xác"));
            // Lấy lên role user
            var listRoles = await _userService.GetListUserRolesByIdAsync(user.Id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetSection("AppConfigs:Secret");
            var key = Encoding.ASCII.GetBytes(secret.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimTypes.UserName, user.UserName.ToString()),
                    new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
                    new Claim(CustomClaimTypes.Email, user.Email.ToString()),
                    new Claim(CustomClaimTypes.ListRole, listRoles),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new AppResponse(ResponseCode.Success, "", new
            {
                user.Id,
                Username = user.UserName,
                user.Email,
                Token = tokenString
            }));
        }

        /// <summary>
        /// Lấy về thông tin user bao gồm cả quyền
        /// nvkhai 18.11.2020
        /// </summary>
        /// <returns></returns>
        [HttpGet("users/me")]
        [Authorize]
        public async Task<ActionResult> GetMe()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaimTypes.UserId).Value);
            if (userId == Guid.Empty || User == null)
            {
                return Ok(new AppResponse(ResponseCode.ValidationError, "Không tìm thấy user."));
            }
            var user = _userService.GetUserById(userId);
            var userClient = await _userService.MapUserToUserClient(user);
            return Ok(new AppResponse(ResponseCode.Success, "", userClient));
        }

        /// <summary>
        /// validate token cho client
        /// nvkhai 18.11.2020
        /// </summary>
        /// <returns></returns>
        [HttpGet("authen")]
        [Authorize]
        public ActionResult Authen()
        {
            return Ok();
        }

        #region Groups

        /// <summary>
        /// Thêm User là quản trị hệ thống của Group
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="userRegisterReq"></param>
        /// <returns></returns>
        [HttpPost("groups/admins")]
        [Authorize]
        public ActionResult AddAdminUserForGroup(UserRegisterReq userRegisterReq)
        {
            var listRoles = HttpContext.User.FindFirst(CustomClaimTypes.ListRole).Value;
            var roleArr = listRoles.ToString().Split("|");
            if (!roleArr.Contains<string>(RoleTypeNameEnums.SYS_ADMIN_G12))
            {
                return Ok(new AppResponse(ResponseCode.NotPermission, "Chỉ quản trị hệ thống có thể thực hiện yêu cầu này."));
            }
            var groupAdmin = _userService.CreateUser(userRegisterReq);
            return Ok(new AppResponse(ResponseCode.Success, "", groupAdmin));
        }

        /// <summary>
        /// Thêm Group
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="userRegisterReq"></param>
        /// <returns></returns>
        [HttpPost("groups")]
        [Authorize]
        public async Task<ActionResult> AddGroup(AddGroupReq addGroupReq)
        {
            var listRoles = HttpContext.User.FindFirst(CustomClaimTypes.ListRole).Value;
            var roleArr = listRoles.ToString().Split("|");
            if (!roleArr.Contains<string>(RoleTypeNameEnums.SYS_ADMIN_G12))
            {
                return Ok(new AppResponse(ResponseCode.NotPermission, "Chỉ quản trị hệ thống có thể thực hiện yêu cầu này."));
            }
            var result = await _groupService.AddGroup(addGroupReq);
            return Ok(result);

        }

        /// <summary>
        /// Thêm Group
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="userRegisterReq"></param>
        /// <returns></returns>
        [HttpPut("groups/{groupId}")]
        [Authorize]
        public async Task<ActionResult> ModifiGroup(AddGroupReq addGroupReq, int groupId)
        {
            var listRoles = HttpContext.User.FindFirst(CustomClaimTypes.ListRole).Value;
            var roleArr = listRoles.ToString().Split("|");
            if (!roleArr.Contains<string>(RoleTypeNameEnums.SYS_ADMIN_G12))
            {
                return Ok(new AppResponse(ResponseCode.NotPermission, "Chỉ quản trị hệ thống có thể thực hiện yêu cầu này."));
            }
            var result = await _groupService.ModifiGroup(addGroupReq, groupId);
            return Ok(result);

        }

        #endregion

    }
}
