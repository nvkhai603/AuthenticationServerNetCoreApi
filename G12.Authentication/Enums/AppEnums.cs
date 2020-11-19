using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Enums
{
    public class ResponseCode
    {
        public static int Success = 200;
        public static int ValidationError = 400;
        public static int NotPermission = 403;
        public static int Exception = 500;
        public static int IntergratesFail = 300;
    }
}
