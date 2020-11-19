using G12.Authentication.Entities;
using G12.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Interfaces
{
    public interface IUserService
    {
        public Task<AppResponse> CreateUser(UserRegisterReq userRegisterReq, bool IsAdminGroup = false);

        public Task<User> Authenticate(UserLoginReq userLoginReq);
        public User GetUserById(Guid Id);
        public Task<string> GetListUserRolesByIdAsync(Guid Id);
        public Task<UserClientRes> MapUserToUserClient(User user);
    }
}
