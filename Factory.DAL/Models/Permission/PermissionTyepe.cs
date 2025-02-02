namespace Factory.DAL.Models.Permission
{
    public class PermissionTyepe
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
