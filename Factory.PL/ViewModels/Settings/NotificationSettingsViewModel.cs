using Factory.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Settings
{
    public class NotificationSettingsViewModel
    {
        [Display(Name = "Enable Email Notifications")]
        public bool EnableEmailNotifications { get; set; }

        [Display(Name = "Enable SMS Notifications")]
        public bool EnableSmsNotifications { get; set; }

        [Display(Name = "Enable Push Notifications")]
        public bool EnablePushNotifications { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Notification frequency is required.")]
        [Display(Name = "Notification Frequency")]
        public NotificationFrequency Frequency { get; set; }
    }
}