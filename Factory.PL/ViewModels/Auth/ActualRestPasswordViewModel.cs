using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class ActualRestPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your new password.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]

        public string UserId { get; set; } = string.Empty;

    }
}
