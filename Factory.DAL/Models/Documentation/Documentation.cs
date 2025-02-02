namespace Factory.DAL.Models.Documentation
{
    public class Documentation
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}