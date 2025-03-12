namespace Factory.PL.ViewModels.Permission
{
    public class SubModuleViewModel
    {
        public int SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IconClass { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
        public List<PageViewModel> Pages { get; set; } = new List<PageViewModel>();
    }
}