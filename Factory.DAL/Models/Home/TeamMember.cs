
namespace Factory.DAL.Models.Home
{
    public class TeamMember
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string ProfileImageUrl { get; set; } = string.Empty;

        public string IconUrl { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string IpAddress { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public bool IsHidden { get; set; }


        public string FacebookLink { get; set; } = string.Empty;

        public string TwitterLink { get; set; } = string.Empty;

        public string LinkedInLink { get; set; } = string.Empty;

        public string InstagramLink { get; set; } = string.Empty;

        public string YouTubeLink { get; set; } = string.Empty;
    }
}
