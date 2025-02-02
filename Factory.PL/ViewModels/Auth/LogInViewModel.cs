using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "The email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "E-Mail", Prompt = "Enter Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 100 characters.")]
        [Display(Name = "Password", Prompt = "Enter Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
