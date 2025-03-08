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
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    public class ActivityLogViewModel
    {
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
