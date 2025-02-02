namespace Factory.PL.ViewModels.Auth
{
    public class SettingsViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Dictionary<string, string> GeneralSettings { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, bool> NotificationPreferences { get; set; } = new Dictionary<string, bool>();

        public ChangePasswordViewModel? ChangePasswordModel { get; set; } 
    }

    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
