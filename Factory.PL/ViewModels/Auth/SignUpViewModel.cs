using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Please Provide UserName")]
        [MinLength(5, ErrorMessage = "UserName  Must be at least 5 characters")]
        [Display(Name = "User Name", Prompt = "Enter User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Provide PhoneNumber")]
        [Phone]
        [Display(Name = "Phone Number", Prompt = "Enter Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Provide Email Address")]
        [EmailAddress(ErrorMessage = "Please Provide Correct Email")]
        [Display(Name = "E-Mail", Prompt = "Enter Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Provide Password")]
        [DataType(DataType.Password, ErrorMessage = "Please Provide Correct Password")]
        [Display(Name = "Password", Prompt = "Enter Password")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Enter confirmation password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
