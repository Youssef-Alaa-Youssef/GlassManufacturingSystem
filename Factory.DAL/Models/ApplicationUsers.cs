using Microsoft.AspNetCore.Identity;

namespace Factory.DAL.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = "/images/default-profile.png";
        public bool IsMFAEnabled { get; set; } = false;
        public bool IsDarkModeEnabled { get; set; } = false;
        public DateTime? LastBackupDate { get; set; }
        public DateTime? DeleteRequestedOn { get; set; }
    }

}
