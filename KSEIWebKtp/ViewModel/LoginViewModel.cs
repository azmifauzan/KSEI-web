﻿using System.ComponentModel.DataAnnotations;

namespace KSEIWebKtp.ViewModel
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Ingat Saya")]
        public bool RememberMe { get; set; }
    }
}
