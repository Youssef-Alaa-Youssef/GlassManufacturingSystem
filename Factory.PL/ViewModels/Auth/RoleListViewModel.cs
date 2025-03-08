namespace Factory.PL.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class RoleListViewModel
    {
        public IEnumerable<RoleViewModel> Roles { get; set; } = Array.Empty<RoleViewModel>();
    }
}
