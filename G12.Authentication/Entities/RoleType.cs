using System;
using System.Collections.Generic;

namespace G12.Authentication.Entities
{
    public partial class RoleType
    {
        public RoleType()
        {
            Role = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Role> Role { get; set; }
    }
}
