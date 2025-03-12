namespace Factory.PL.ViewModels.Permissions
{
    public class PermissionManagementViewModel
    {
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public List<ModuleViewModel> Modules { get; set; } = new List<ModuleViewModel>();
        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
        public List<RolePermissionViewModel> RolePermissions { get; set; } = new List<RolePermissionViewModel>();
    }
}
