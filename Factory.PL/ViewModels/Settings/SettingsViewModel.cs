using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Settings
{
    public class SettingsViewModel
    {
        public int Id { get; set; }

        // Factory Information
        [Display(Name = "Factory Name (Arabic)")]
        [Required(ErrorMessage = "Factory Name (Arabic) is required.")]
        public string FactoryNameAr { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "Factory Name (English)")]
        [Required(ErrorMessage = "Factory Name (English) is required.")]
        public string FactoryNameEn { get; set; } = string.Empty;

        [Display(Name = "Factory Logo")]
        public string FactoryLogo { get; set; } = string.Empty;

        [Display(Name = "Tax Number")]
        [Required(ErrorMessage = "Tax Number is required.")]
        public string TaxNumber { get; set; } = string.Empty;

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required.")]
        public string ContactNumber { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Website")]
        [Url(ErrorMessage = "Invalid Website URL.")]
        public string Website { get; set; } = string.Empty;

        // Social Media Links
        [Display(Name = "Facebook URL")]
        [Url(ErrorMessage = "Invalid Facebook URL.")]
        public string FacebookUrl { get; set; } = string.Empty;

        [Display(Name = "Twitter URL")]
        [Url(ErrorMessage = "Invalid Twitter URL.")]
        public string TwitterUrl { get; set; } = string.Empty;

        [Display(Name = "LinkedIn URL")]
        [Url(ErrorMessage = "Invalid LinkedIn URL.")]
        public string LinkedInUrl { get; set; } = string.Empty;

        [Display(Name = "Instagram URL")]
        [Url(ErrorMessage = "Invalid Instagram URL.")]
        public string InstagramUrl { get; set; } = string.Empty;

        // Display Settings
        [Display(Name = "Theme")]
        public string Theme { get; set; } = "Light"; // Light, Dark

        [Display(Name = "Language")]
        public string Language { get; set; } = "English"; // English, Arabic

        [Display(Name = "Items Per Page")]
        [Range(5, 100, ErrorMessage = "Items per page must be between 5 and 100.")]
        public int ItemsPerPage { get; set; } = 10;

        [Display(Name = "Enable Animations")]
        public bool EnableAnimations { get; set; } = true;

        // Notification Settings
        [Display(Name = "Enable Notifications")]
        public bool EnableNotifications { get; set; } = true;

        [Display(Name = "Notification Preferences")]
        public string NotificationPreferences { get; set; } = "Email"; // Email, SMS, Both

        [Display(Name = "Notify on New Order")]
        public bool NotifyOnNewOrder { get; set; } = true;

        [Display(Name = "Notify on Delivery")]
        public bool NotifyOnDelivery { get; set; } = true;

        // Design Settings
        [Display(Name = "Show Design 1")]
        public bool ShowDesign1 { get; set; } = true;

        [Display(Name = "Show Design 2")]
        public bool ShowDesign2 { get; set; } = true;

        [Display(Name = "Show Design 3")]
        public bool ShowDesign3 { get; set; } = true;

        [Display(Name = "Default Design")]
        public string DefaultDesign { get; set; } = "Design1"; // Design1, Design2, Design3

        // Security Settings
        [Display(Name = "Enable Two-Factor Authentication")]
        public bool EnableTwoFactorAuth { get; set; } = false;

        [Display(Name = "Password Expiry (Days)")]
        [Range(30, 365, ErrorMessage = "Password expiry must be between 30 and 365 days.")]
        public int PasswordExpiryDays { get; set; } = 90;

        [Display(Name = "Require Strong Passwords")]
        public bool RequireStrongPasswords { get; set; } = true;

        // Backup Settings
        [Display(Name = "Enable Auto Backup")]
        public bool EnableAutoBackup { get; set; } = true;

        [Display(Name = "Backup Frequency")]
        public string BackupFrequency { get; set; } = "Daily"; // Daily, Weekly, Monthly

        [Display(Name = "Backup Location")]
        public string BackupLocation { get; set; } = "Cloud"; // Local, Cloud

        // Audit Logs
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; } = string.Empty;

        [Display(Name = "Last Modified At")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

        // Timestamps
        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated At")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}