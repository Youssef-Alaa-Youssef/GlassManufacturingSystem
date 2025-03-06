using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Auth
{
    public class UpdateRoleModel
    {
        [Required]

        public string Role { get; set; } = string.Empty;
    }
}
