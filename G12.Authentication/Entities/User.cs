using System;
using System.Collections.Generic;

namespace G12.Authentication.Entities
{
    public partial class User
    {
        public User()
        {
            Role = new HashSet<Role>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Class { get; set; }
        public bool? Active { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Modifiedby { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Role> Role { get; set; }
    }
}
