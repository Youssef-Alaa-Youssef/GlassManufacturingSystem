namespace Factory.PL.ViewModels.Auth
{
    public class TwoFactorSettingsViewModel
    {
        public bool IsTwoFactorEnabled { get; set; }
        public bool IsAuthenticatorAppEnabled { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
