﻿using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace CAHM.ViewModels
{
    public class ResetPasswordModel
    {

        [Required]
        public string RequestId { get; set; }

        [Required, Email]
        public string Email { get; set; }

        [Required]
        public string RequestHash { get; set; }

        [Required, StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Minimum password length is 5.")]
        public string Password { get; set; }

        [Required, EqualTo("Password", ErrorMessage = "Passwords don't match. Try again.")]
        public string ConfirmPassword { get; set; }

    }
}
