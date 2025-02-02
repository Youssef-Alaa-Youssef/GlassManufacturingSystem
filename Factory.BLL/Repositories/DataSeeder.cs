using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Factory.DAL.Enums;
using Factory.DAL.Models.Permission;
using Factory.BLL.InterFaces;

namespace Factory.DAL.Configurations
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            // Seed roles
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                string roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed default user
            var defaultUserEmail = "superadmin@gmail.com";
            var defaultUser = await userManager.FindByEmailAsync(defaultUserEmail);

            if (defaultUser == null)
            {
                defaultUser = new IdentityUser
                {
                    UserName = defaultUserEmail,
                    Email = defaultUserEmail
                };

                var result = await userManager.CreateAsync(defaultUser, "SuperAdmin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, UserRole.SuperAdmin.ToString());
                    defaultUser.EmailConfirmed = true;
                    await userManager.UpdateAsync(defaultUser);
                }
            }

            var permissionRepository = unitOfWork.GetRepository<PermissionTyepe>();
            
            var superAdminRole = await roleManager.FindByNameAsync(UserRole.SuperAdmin.ToString());
            var managePermissions = permissionRepository.GetAllAsync().Result.FirstOrDefault(p => p.Name == "ManagePermissions");

            if (superAdminRole != null && managePermissions != null)
            {
                var rolePermissionRepository = unitOfWork.GetRepository<RolePermission>();
                var rolePermission = new RolePermission
                {
                    RoleId = superAdminRole.Id,
                    PermissionId = managePermissions.Id,
                    ModuleId = 1 
                };

                var existingRolePermission = rolePermissionRepository.GetAllAsync()
                    .Result.FirstOrDefault(rp =>
                        rp.RoleId == rolePermission.RoleId &&
                        rp.PermissionId == rolePermission.PermissionId &&
                        rp.ModuleId == rolePermission.ModuleId);

                if (existingRolePermission == null)
                {
                    await rolePermissionRepository.AddAsync(rolePermission);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}