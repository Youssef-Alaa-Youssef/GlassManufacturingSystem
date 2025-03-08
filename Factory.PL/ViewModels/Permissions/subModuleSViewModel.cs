using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.PL.ViewModels.Permission
{
    public class SubModulesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public string IconClass { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> Modules { get; set; } = new List<SelectListItem>();
    }
}