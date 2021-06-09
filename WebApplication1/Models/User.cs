using System;
using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class User : IdentityUser
    {
       // public string Id { get; set; }
        public string uname { get; set; }
        public bool isBlock { get; set; }
        public virtual DateTime? tlog { get; set; }
        public virtual DateTime? treg { get; set; }
        public bool isActive { get; set; }
    }
}