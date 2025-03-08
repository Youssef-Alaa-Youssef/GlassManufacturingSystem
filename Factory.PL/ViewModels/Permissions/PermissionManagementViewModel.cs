namespace Factory.PL.ViewModels.Permissions
{
    public class PermissionManagementViewModel
    {
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>(); // List of users and their roles
        public List<ModuleViewModel> Modules { get; set; } = new List<ModuleViewModel>(); // List of all modules
        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>(); // List of all permissions
        public List<RolePermissionViewModel> RolePermissions { get; set; } = new List<RolePermissionViewModel>(); // List of roles and their permissions
    }
}
