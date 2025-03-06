using Factory.DAL.Models.Auth;

namespace Factory.DAL.Models.Support
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; } = "Open";
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
        public string Type { get; set; } = "General";
        public DateTime? ResolvedAt { get; set; }
        public string AssignedToUserId { get; set; } = string.Empty;
        public virtual ApplicationUser? AssignedToUser { get; set; }
        public virtual ICollection<SupportResponse> Responses { get; set; } = new List<SupportResponse>();
    }
}