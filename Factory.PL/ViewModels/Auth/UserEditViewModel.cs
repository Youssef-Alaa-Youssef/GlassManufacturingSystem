using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class UserEditViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Active Now")]
        public bool IsActive { get; set; }
    }
}
