using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Factory.DAL.Configurations
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                string roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var defaultUserEmail = "superadmin@gmail.com";
            var defaultUser = await userManager.FindByEmailAsync(defaultUserEmail);

            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser
                {
                    UserName = defaultUserEmail,
                    Email = defaultUserEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(defaultUser, "SuperAdmin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, UserRole.SuperAdmin.ToString());
                }
            }

            var permissionRepository = unitOfWork.GetRepository<PermissionTyepe>();
            var existingPermissions = await permissionRepository.GetAllAsync();

            var requiredPermissions = new List<string> { "Delete", "Read", "Create", "Update" };

            var newPermissions = requiredPermissions
                .Where(permissionName => !existingPermissions.Any(p => p.Name == permissionName))
                .Select(permissionName => new PermissionTyepe { Name = permissionName })
                .ToList();

            if (newPermissions.Any())
            {
                await permissionRepository.AddRangeAsync(newPermissions);
                await unitOfWork.SaveChangesAsync();
            }

            var moduleRepository = unitOfWork.GetRepository<Module>();
            var allModules = await moduleRepository.GetAllAsync();

            var superAdminRole = await roleManager.FindByNameAsync(UserRole.SuperAdmin.ToString());

            if (superAdminRole != null && allModules.Any())
            {
                var rolePermissionRepository = unitOfWork.GetRepository<RolePermission>();
                var allPermissions = await permissionRepository.GetAllAsync();
                var existingRolePermissions = await rolePermissionRepository.GetAllAsync();

                var newRolePermissions = new List<RolePermission>();

                foreach (var module in allModules)
                {
                    foreach (var permission in allPermissions)
                    {
                        if (!existingRolePermissions.Any(rp => rp.RoleId == superAdminRole.Id &&
                                                               rp.PermissionId == permission.Id &&
                                                               rp.ModuleId == module.Id))
                        {
                            newRolePermissions.Add(new RolePermission
                            {
                                RoleId = superAdminRole.Id,
                                PermissionId = permission.Id,
                                ModuleId = module.Id
                            });
                        }
                    }
                }

                if (newRolePermissions.Any())
                {
                    await rolePermissionRepository.AddRangeAsync(newRolePermissions);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}
