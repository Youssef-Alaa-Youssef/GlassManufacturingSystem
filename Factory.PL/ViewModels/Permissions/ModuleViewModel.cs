using Factory.PL.ViewModels.Permission;

namespace Factory.PL.ViewModels.Permissions
{
    public class ModuleViewModel
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public List<SubModuleViewModel> SubModules { get; set; } = new List<SubModuleViewModel>();
    }
}