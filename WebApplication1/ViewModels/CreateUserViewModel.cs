﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
    }
}