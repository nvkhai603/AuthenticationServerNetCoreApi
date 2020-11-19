using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Models
{
    public class UserClientRes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Class { get; set; }
        public bool? Active { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ListRoles { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Modifiedby { get; set; }
    }
}
