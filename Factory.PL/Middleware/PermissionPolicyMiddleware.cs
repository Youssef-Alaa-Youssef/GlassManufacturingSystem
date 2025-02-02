using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using Factory.BLL.InterFaces;

namespace Factory.PL.Helper
{
    public class PermissionPolicyMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionPolicyMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork, IAuthorizationPolicyProvider policyProvider)
        {
            // Fetch permissions from the database
            var permissions = await unitOfWork.GetRepository<PermissionTyepe>().GetAllAsync();

            // Register each permission as a policy
            if (policyProvider is CustomAuthorizationPolicyProvider customPolicyProvider)
            {
                foreach (var permission in permissions)
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireClaim("Permission", permission.Name) // This assumes permissions are stored as claims
                        .Build();

                    customPolicyProvider.AddPolicy(permission.Name, policy);
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

}
