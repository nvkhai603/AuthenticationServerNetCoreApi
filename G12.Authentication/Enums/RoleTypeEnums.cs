using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Enums
{
    public class RoleTypeEnums
    {
        public static int SYS_ADMIN = 1;
        public static int GROUP_ADMIN = 2;
        public static int GROUP_USER = 3;
    }

    public class RoleTypeNameEnums
    {
        public static string SYS_ADMIN = "SYS_ADMIN";
        public static string GROUP_ADMIN = "GROUP_ADMIN";
        public static string GROUP_USER = "GROUP_USER";
        public static string SYS_ADMIN_G12 = "SYS_ADMIN/GROUP12";
    }
}
