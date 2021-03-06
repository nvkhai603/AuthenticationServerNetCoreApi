﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G12.Authentication.Models
{
    public class AddGroupReq
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlInstall { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string UrlHome { get; set; }
        [Required]
        public string Avatar { get; set; }
    }
}
