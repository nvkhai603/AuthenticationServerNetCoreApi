using System;
using System.Collections.Generic;

namespace G12.Authentication.Entities
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public int RoleType { get; set; }
        public int RoleGroup { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid UserId { get; set; }

        public virtual GroupInfor RoleGroupNavigation { get; set; }
        public virtual RoleType RoleTypeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
