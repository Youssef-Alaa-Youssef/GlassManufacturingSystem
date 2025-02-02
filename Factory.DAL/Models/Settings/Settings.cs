namespace Factory.DAL.Models.Settings
{
    public class Settings
    {
        public int Id { get; set; }

        // Factory Information
        public string FactoryNameAr { get; set; } = string.Empty; // Factory Name in Arabic
        public string UserId { get; set; } = string.Empty; // Factory Name in Arabic
        public string FactoryNameEn { get; set; } = string.Empty; // Factory Name in English
        public string FactoryLogo { get; set; } = string.Empty; // URL or base64 for the logo
        public string TaxNumber { get; set; } = string.Empty; // Tax Identification Number
        public string Address { get; set; } = string.Empty; // Factory Address
        public string ContactNumber { get; set; } = string.Empty; // Primary Contact Number
        public string Email { get; set; } = string.Empty; // Factory Email
        public string Website { get; set; } = string.Empty; // Factory Website URL

        // Social Media Links
        public string FacebookUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;

        // Display Settings
        public string Theme { get; set; } = "Light"; // Light, Dark, Custom
        public string Language { get; set; } = "English"; // English, Arabic
        public int ItemsPerPage { get; set; } = 10; // Pagination Settings
        public bool EnableAnimations { get; set; } = true; // Enable/Disable UI Animations

        // Notification Settings
        public bool EnableNotifications { get; set; } = true; // Enable/Disable Notifications
        public string NotificationPreferences { get; set; } = "Email"; // Email, SMS, Both
        public bool NotifyOnNewOrder { get; set; } = true; // Notify when a new order is placed
        public bool NotifyOnDelivery { get; set; } = true; // Notify when an order is delivered

        // Design Settings
        public bool ShowDesign1 { get; set; } = true; // Show/Hide Design 1
        public bool ShowDesign2 { get; set; } = true; // Show/Hide Design 2
        public bool ShowDesign3 { get; set; } = true; // Show/Hide Design 3
        public string DefaultDesign { get; set; } = "Design1"; // Default Design (Design1, Design2, Design3)

        // Security Settings
        public bool EnableTwoFactorAuth { get; set; } = false; // Enable Two-Factor Authentication
        public int PasswordExpiryDays { get; set; } = 90; // Password Expiry in Days
        public bool RequireStrongPasswords { get; set; } = true; // Require Strong Passwords

        // Backup Settings
        public bool EnableAutoBackup { get; set; } = true; // Enable Automatic Backups
        public string BackupFrequency { get; set; } = "Daily"; // Daily, Weekly, Monthly
        public string BackupLocation { get; set; } = "Cloud"; // Local, Cloud

        // Audit Logs
        public string LastModifiedBy { get; set; } = string.Empty; // User who last modified the settings
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow; // Last modification timestamp

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Record Creation Time
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last Update Time
    }
}
