using G12.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Interfaces
{
    public interface IGroupService
    {
        public bool IsGroupExist(string gName, string gCode);
        public Task<AppResponse> AddGroup(AddGroupReq addGroupReq);
        public Task<AppResponse> ModifiGroup(AddGroupReq addGroupReq, int groupId);
    }
}
