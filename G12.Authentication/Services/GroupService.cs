using G12.Authentication.Entities;
using G12.Authentication.Enums;
using G12.Authentication.Interfaces;
using G12.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Services
{
    public class GroupService : IGroupService
    {
        private readonly AuthenticationDbContext _dbContext;
        public GroupService(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Thêm mới một Group
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="addGroupReq"></param>
        /// <returns></returns>
        public async Task<AppResponse> AddGroup(AddGroupReq addGroupReq)
        {
            if (IsGroupExist(addGroupReq.Name, addGroupReq.Code))
            {
                return new AppResponse(ResponseCode.ValidationError, "Group đã tồn tại");
            }
            var newGroup = new GroupInfor
            {
                Code = addGroupReq.Code,
                Name = addGroupReq.Name,
                UrlHome = addGroupReq.UrlHome,
                UrlInstall = addGroupReq.UrlInstall,
                Avatar = addGroupReq.Avatar,
                CreatedDate = DateTime.Now,
                CreatedBy = RoleTypeNameEnums.SYS_ADMIN
            };

            await _dbContext.GroupInfor.AddAsync(newGroup);
            await _dbContext.SaveChangesAsync();
            return new AppResponse(ResponseCode.Success, "", newGroup);
        }

        /// <summary>
        /// Chỉnh sửa, cập nhật một group
        /// nvkhai 18.11.2020
        /// </summary>
        /// <param name="addGroupReq"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<AppResponse> ModifiGroup(AddGroupReq addGroupReq, int groupId)
        {
            var group = await _dbContext.GroupInfor.FindAsync(groupId);
            if (group == null)
            {
                return new AppResponse(ResponseCode.ValidationError, "Group không tồn tại.");
            }

            group.Code = addGroupReq.Code;
            group.Name = addGroupReq.Name;
            group.UrlHome = addGroupReq.UrlHome;
            group.UrlInstall = addGroupReq.UrlInstall;
            group.ModifiedDate = DateTime.Now;
            group.Avatar = addGroupReq.Avatar;
            group.ModifiedBy = RoleTypeNameEnums.SYS_ADMIN;

            await _dbContext.SaveChangesAsync();
            return new AppResponse(ResponseCode.Success, "", group);
        }


        public bool IsGroupExist(string gName, string gCode)
        {
            return _dbContext.GroupInfor.Any(g => g.Name == gName || g.Code == gCode);
        }

        public AppResponse GetAllGroups()
        {
            var groups = _dbContext.GroupInfor;
            var groupsRes = new List<GroupClientRes>();
            foreach (var group in groups)
            {
                groupsRes.Add(new GroupClientRes { Id = group.Id, Code = group.Code, Name = group.Name, Avatar = group.Avatar, UrlHome = group.UrlHome });
            }
            return new AppResponse(ResponseCode.Success, "", groupsRes);
        }
    }
}
