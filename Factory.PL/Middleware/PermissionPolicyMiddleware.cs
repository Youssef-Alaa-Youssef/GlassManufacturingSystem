using Microsoft.Extensions.Caching.Memory;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Concurrent;
using Factory.BLL.InterFaces;

namespace Factory.PL.Helper
{
    public class PermissionPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionPolicyMiddleware> _logger;
        private readonly IMemoryCache _memoryCache;

        // Cache key prefixes
        private const string UserRolesCacheKeyPrefix = "UserRoles_";
        private const string RolePermissionsCacheKey = "RolePermissions";
        private const string PermissionsCacheKey = "Permissions";
        private const string ModulesCacheKey = "Modules";

        private static readonly TimeSpan CacheExpiration = TimeSpan.FromDays(10);
        private static readonly ConcurrentDictionary<string, AuthorizationPolicy> PolicyCache = new();

        public PermissionPolicyMiddleware(
            RequestDelegate next,
            ILogger<PermissionPolicyMiddleware> logger,
            IMemoryCache memoryCache)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUnitOfWork unitOfWork,
            IAuthorizationPolicyProvider policyProvider,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                try
                {
                    var userRolesCacheKey = $"{UserRolesCacheKeyPrefix}{userId}";
                    if (!_memoryCache.TryGetValue(userRolesCacheKey, out List<string> roleNames))
                    {
                        var user = await userManager.FindByIdAsync(userId);
                        if (user == null)
                        {
                            _logger.LogWarning($"User with ID {userId} not found.");
                            await _next(context);
                            return;
                        }

                        roleNames = (await userManager.GetRolesAsync(user)).ToList();
                        _memoryCache.Set(userRolesCacheKey, roleNames, CacheExpiration); // Use constant expiration
                    }

                    if (roleNames.Count == 0)
                    {
                        _logger.LogWarning($"User {userId} has no assigned roles.");
                        await _next(context);
                        return;
                    }

                    var roleIds = await _memoryCache.GetOrCreateAsync("RoleIds", async entry =>
                    {
                        entry.SlidingExpiration = CacheExpiration;
                        return await roleManager.Roles
                            .Where(r => roleNames.Contains(r.Name))
                            .Select(r => r.Id)
                            .ToListAsync();
                    });

                    var userRolePermissions = await _memoryCache.GetOrCreateAsync(RolePermissionsCacheKey, async entry =>
                    {
                        entry.SlidingExpiration = CacheExpiration;
                        return await unitOfWork.GetRepository<RolePermission>()
                            .FindAsync(rp => roleIds.Contains(rp.RoleId));
                    });

                    var permissions = await _memoryCache.GetOrCreateAsync(PermissionsCacheKey, async entry =>
                    {
                        entry.SlidingExpiration = CacheExpiration;
                        return await unitOfWork.GetRepository<PermissionTyepe>().GetAllAsync();
                    });

                    var modules = await _memoryCache.GetOrCreateAsync(ModulesCacheKey, async entry =>
                    {
                        entry.SlidingExpiration = CacheExpiration;
                        return await unitOfWork.GetRepository<Module>().GetAllAsync();
                    });

                    if (policyProvider is CustomAuthorizationPolicyProvider customPolicyProvider)
                    {
                        foreach (var permission in permissions)
                        {
                            foreach (var module in modules)
                            {
                                string policyName = $"{module.Name}_{permission.Name}";

                                if (!PolicyCache.TryGetValue(policyName, out var policy))
                                {
                                    policy = CreatePolicy(userRolePermissions, permission, module);
                                    PolicyCache.TryAdd(policyName, policy);
                                }

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