using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Models
{
    public class AppResponse
    {

        public AppResponse(int code, string message, object data = null)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
