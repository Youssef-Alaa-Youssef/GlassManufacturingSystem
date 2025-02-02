using Microsoft.AspNetCore.Identity;

namespace Factory.DAL.Models.Permission
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public int PermissionId { get; set; }
        public int ModuleId { get; set; }

        public virtual IdentityRole Role { get; set; }
        public virtual PermissionTyepe Permission { get; set; }
        public virtual Module Module { get; set; }
    }
}