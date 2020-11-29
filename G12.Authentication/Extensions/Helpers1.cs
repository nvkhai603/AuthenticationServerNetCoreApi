using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Extensions
{
    public class Helpers
    {
        public static string GenerateSid()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
    }
}
