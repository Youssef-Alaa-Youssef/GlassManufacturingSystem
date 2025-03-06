using Factory.DAL.Models.Auth;

namespace Factory.DAL.Models.Support
{
    public class SupportResponse
    {
        public int Id { get; set; }
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int SupportTicketId { get; set; }
        public virtual SupportTicket SupportTicket { get; set; } = new SupportTicket();
        public string? RespondedByUserId { get; set; } = string.Empty;
        public virtual ApplicationUser? RespondedByUser { get; set; }
    }
}