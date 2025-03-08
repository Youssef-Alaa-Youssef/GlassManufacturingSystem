using Factory.DAL.Enums;
using System.ComponentModel.DataAnnotations;
namespace Factory.DAL.Models.Settings
{
    public class NotificationSettings
    {
        public int Id { get; set; }

        [Required]
        public bool EnableEmailNotifications { get; set; }

        [Required]
        public bool EnableSmsNotifications { get; set; }

        [Required]
        public bool EnablePushNotifications { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public NotificationFrequency Frequency { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
