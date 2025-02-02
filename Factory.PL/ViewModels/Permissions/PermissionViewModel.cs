namespace Factory.PL.ViewModels.Permissions
{
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public List<ModuleViewModel> Modules { get; set; } = new List<ModuleViewModel>();
    }
}