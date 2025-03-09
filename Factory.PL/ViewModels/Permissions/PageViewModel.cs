namespace Factory.PL.ViewModels.Permission
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SubmoduleId { get; set; }
    }
}