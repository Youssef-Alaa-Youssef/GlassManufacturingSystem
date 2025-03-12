using Factory.DAL.Models.Permission;

namespace Factory.PL.ViewModels.Permission
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string SecureUrlKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public SubModule SubmoduleId { get; set; }
    }
}