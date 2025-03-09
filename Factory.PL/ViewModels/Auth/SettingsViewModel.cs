using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class SettingsViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;

        public bool IsMFAEnabled { get; set; }

        public bool IsDarkModeEnabled { get; set; }

        public List<ActivityLogViewModel> RecentActivities { get; set; } = new();

        public DateTime? LastBackupDate { get; set; }

        public ChangePasswordViewModel? ChangePasswordModel { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [DisplayName("Current Password")]
        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [DisplayName("New Password")]
        [Required(ErrorMessage = "New password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; } = string.Empty;

        [DisplayName("Confirm New Password")]
        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
    public class ActivityLogViewModel
    {
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
