namespace Factory.PL.ViewModels.Permission
{
    public class SubModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int ModuleId { get; set; }
    }
}