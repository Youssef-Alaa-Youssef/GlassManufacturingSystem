using Factory.DAL.Models.Permission;

namespace Factory.PL.ViewModels.Permission
{
    public class ModulesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();
    }
}