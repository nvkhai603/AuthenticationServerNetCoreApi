using System;
using System.Collections.Generic;

namespace G12.Authentication.Entities
{
    public partial class GroupInfor
    {
        public GroupInfor()
        {
            Role = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlInstall { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Code { get; set; }
        public string UrlHome { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Role> Role { get; set; }
    }
}
