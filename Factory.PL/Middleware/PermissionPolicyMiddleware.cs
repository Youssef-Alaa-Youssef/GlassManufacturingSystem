using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Factory.PL.Helper
{
    public class PermissionPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionPolicyMiddleware> _logger;

        public PermissionPolicyMiddleware(RequestDelegate next, ILogger<PermissionPolicyMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUnitOfWork unitOfWork,
            IAuthorizationPolicyProvider policyProvider,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        _logger.LogWarning($"User with ID {userId} not found.");
                        await _next(context);
                        return;
                    }

                    var roleNames = await userManager.GetRolesAsync(user);
                    if (roleNames.Count == 0)
                    {
                        _logger.LogWarning($"User {user.UserName} has no assigned roles.");
                        await _next(context);
                        return;
                    }

                    var roleIds = await roleManager.Roles
                        .Where(r => roleNames.Contains(r.Name))
                        .Select(r => r.Id)
                        .ToListAsync();

                    var userRolePermissions = await unitOfWork.GetRepository<RolePermission>()
                        .FindAsync(rp => roleIds.Contains(rp.RoleId));

                    var permissions = await unitOfWork.GetRepository<PermissionTyepe>().GetAllAsync();
                    var modules = await unitOfWork.GetRepository<Module>().GetAllAsync();

                    if (policyProvider is CustomAuthorizationPolicyProvider customPolicyProvider)
                    {
                        foreach (var permission in permissions)
                        {
                            foreach (var module in modules)
                            {
                                string policyName = $"{module.Name}_{permission.Name}";

                                var policy = CreatePolicy(userRolePermissions, permission, module);
                                customPolicyProvider.AddPolicy(policyName, policy);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating policies.");
                }
            }

            await _next(context);
        }

        private AuthorizationPolicy CreatePolicy(
            IEnumerable<RolePermission> userRolePermissions,
            PermissionTyepe permission,
            Module module)
        {
            return new AuthorizationPolicyBuilder()
                .RequireAssertion(ctx =>
                {
                    var hasPermission = userRolePermissions.Any(rp =>
                        rp.PermissionId == permission.Id &&
                        rp.ModuleId == module.Id);

                    return hasPermission;
                })
                .Build();
        }
    }
}