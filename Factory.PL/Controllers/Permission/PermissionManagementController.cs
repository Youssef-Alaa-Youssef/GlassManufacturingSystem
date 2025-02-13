using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Factory.PL.ViewModels.Permissions;
using Factory.BLL.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Factory.PL.Controllers.Permission
{
    public class PermissionManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PermissionManagementController(
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Policy = "Permission Management_Read")]
        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.GetRepository<IdentityUser>().GetAllAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            var permissions = await _unitOfWork.GetRepository<PermissionTyepe>().GetAllAsync();
            var rolePermissions = await _unitOfWork.GetRepository<RolePermission>().GetAllAsync();

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles.ToList()
                });
            }

            var moduleViewModels = modules.Select(module => new ModuleViewModel
            {
                ModuleId = module.Id,
                ModuleName = module.Name,
                IsSelected = false 
            }).ToList();

            var permissionViewModels = permissions.Select(permission => new PermissionViewModel
            {
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                Modules = moduleViewModels.Select(module => new ModuleViewModel
                {
                    ModuleId = module.ModuleId,
                    ModuleName = module.ModuleName,
                    IsSelected = rolePermissions.Any(rp =>
                        rp.PermissionId == permission.Id &&
                        rp.ModuleId == module.ModuleId)
                }).ToList()
            }).ToList();

            var rolePermissionViewModels = roles.Select(role => new RolePermissionViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = permissionViewModels.Select(permission => new PermissionViewModel
                {
                    PermissionId = permission.PermissionId,
                    PermissionName = permission.PermissionName,
                    Modules = permission.Modules.Select(module => new ModuleViewModel
                    {
                        ModuleId = module.ModuleId,
                        ModuleName = module.ModuleName,
                        IsSelected = rolePermissions.Any(rp =>
                            rp.RoleId == role.Id &&
                            rp.PermissionId == permission.PermissionId &&
                            rp.ModuleId == module.ModuleId)
                    }).ToList()
                }).ToList()
            }).ToList();

            var viewModel = new PermissionManagementViewModel
            {
                Users = userViewModels,
                Modules = moduleViewModels,
                Permissions = permissionViewModels,
                RolePermissions = rolePermissionViewModels
            };

            return View(viewModel);
        }


        [Authorize(Policy = "Permission Management_Create")]

        public async Task<IActionResult> AssignPermissions()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            var permissions = await _unitOfWork.GetRepository<PermissionTyepe>().GetAllAsync();
            var rolePermissions = await _unitOfWork.GetRepository<RolePermission>().GetAllAsync();

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles.ToList()
                });
            }

            var moduleViewModels = modules.Select(module => new ModuleViewModel
            {
                ModuleId = module.Id,
                ModuleName = module.Name,
                IsSelected = false
            }).ToList();

            var permissionViewModels = permissions.Select(permission => new PermissionViewModel
            {
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                Modules = moduleViewModels.Select(module => new ModuleViewModel
                {
                    ModuleId = module.ModuleId,
                    ModuleName = module.ModuleName,
                    IsSelected = rolePermissions.Any(rp =>
                        rp.PermissionId == permission.Id &&
                        rp.ModuleId == module.ModuleId)
                }).ToList()
            }).ToList();

            var rolePermissionViewModels = roles.Select(role => new RolePermissionViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = permissionViewModels.Select(permission => new PermissionViewModel
                {
                    PermissionId = permission.PermissionId,
                    PermissionName = permission.PermissionName,
                    Modules = permission.Modules.Select(module => new ModuleViewModel
                    {
                        ModuleId = module.ModuleId,
                        ModuleName = module.ModuleName,
                        IsSelected = rolePermissions.Any(rp =>
                            rp.RoleId == role.Id &&
                            rp.PermissionId == permission.PermissionId &&
                            rp.ModuleId == module.ModuleId)
                    }).ToList()
                }).ToList()
            }).ToList();

            var viewModel = new PermissionManagementViewModel
            {
                Users = userViewModels,
                Modules = moduleViewModels,
                Permissions = permissionViewModels,
                RolePermissions = rolePermissionViewModels
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Permission Management_Create")]

        public async Task<IActionResult> AssignPermissions(PermissionManagementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "Invalid data submitted. Errors: " + string.Join(", ", errors);
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var existingRolePermissions = await _unitOfWork.GetRepository<RolePermission>().GetAllAsync();
                await _unitOfWork.GetRepository<RolePermission>().RemoveRangeAsync(existingRolePermissions);

                foreach (var rolePermissionViewModel in model.RolePermissions)
                {
                    foreach (var permissionViewModel in rolePermissionViewModel.Permissions)
                    {
                        foreach (var moduleViewModel in permissionViewModel.Modules)
                        {
                            if (moduleViewModel.IsSelected)
                            {
                                await _unitOfWork.GetRepository<RolePermission>().AddAsync(new RolePermission
                                {
                                    RoleId = rolePermissionViewModel.RoleId,
                                    PermissionId = permissionViewModel.PermissionId,
                                    ModuleId = moduleViewModel.ModuleId
                                });
                            }
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Permissions updated successfully!";
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while updating permissions. Please try again.";
            }

            return RedirectToAction(nameof(Index));
        }
    
    }
}