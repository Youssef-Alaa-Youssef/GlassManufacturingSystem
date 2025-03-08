using Microsoft.AspNetCore.Identity;

namespace Factory.DAL.Models.Auth
{
    public class ApplicationRoles : IdentityRole
    {
        public String RoleNameAr { get; set; } = string.Empty;
    }
}
