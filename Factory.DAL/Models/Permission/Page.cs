namespace Factory.DAL.Models.Permission
{
    public class Page
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SubmoduleId { get; set; }
        public virtual SubModule Submodule { get; set; }
        public string SecureUrlKey { get; set; } = string.Empty;

    }
}