﻿using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class RestPasswordViewModel
    {
        [Required(ErrorMessage = "Please Provide Email Address")]
        [EmailAddress(ErrorMessage = "Please Provide Correct Email")]
        [Display(Name = "E-Mail", Prompt = "Enter Email Address")]
        public string Email { get; set; } = string.Empty;
    }
}
