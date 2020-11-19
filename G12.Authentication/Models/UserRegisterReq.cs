using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Models
{
    public class UserRegisterReq
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Class { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GroupCode { get; set; }
    }
}
