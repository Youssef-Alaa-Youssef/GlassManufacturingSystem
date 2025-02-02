namespace Factory.PL.ViewModels.Auth
{
    public class AssignUsersViewModel
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public List<UserViewModel> Users { get; set; } = [];
        public List<string> SelectedUsers { get; set; } = [];
    }
}
