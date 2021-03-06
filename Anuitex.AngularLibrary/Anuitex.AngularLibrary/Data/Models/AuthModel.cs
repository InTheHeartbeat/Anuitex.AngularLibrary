﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anuitex.AngularLibrary.Data.Models
{
    public class AuthModel
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsVisitor { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}