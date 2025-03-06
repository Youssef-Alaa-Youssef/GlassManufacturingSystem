using Factory.DAL.Models.Auth;

namespace Factory.DAL.Models.Support
{
    public class FAQS
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ArchivedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Category { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
        public int Views { get; set; }
        public int HelpfulVotes { get; set; }
        public int UnhelpfulVotes { get; set; }
    }
}