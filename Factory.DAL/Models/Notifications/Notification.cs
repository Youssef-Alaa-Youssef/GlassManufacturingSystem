namespace Factory.DAL.Models.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp
        public bool IsRead { get; set; } = false; 
        public string Type { get; set; } 
        public string IconClass { get; set; } 
        public string UserId { get; set; } 
    }
}
