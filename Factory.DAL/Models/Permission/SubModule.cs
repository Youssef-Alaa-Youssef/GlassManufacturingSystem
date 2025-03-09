namespace Factory.DAL.Models.Permission
{
    public class SubModule
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    }
}