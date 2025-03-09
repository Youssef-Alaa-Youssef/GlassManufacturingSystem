namespace Factory.DAL.Models.Permission
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();
    }
}
