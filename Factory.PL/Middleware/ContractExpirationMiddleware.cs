using Factory.DAL;
using Factory.DAL.Models.Settings;
using System.Security.Claims;

namespace Factory.PL.Middleware
{
    public class ContractExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public ContractExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, FactDdContext dbContext)
        {
            var contract = dbContext.Set<ContractSettings>().FirstOrDefault();
            var user = context.User; // Get authenticated user
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value; // Extract user email

            bool isSuperAdmin = userEmail != null && userEmail.Equals("Superadmin@gmail.com", StringComparison.OrdinalIgnoreCase);
            bool hasRedirected = context.Request.Cookies.ContainsKey("HasRedirected");

            if (contract != null && !contract.IsActive && DateTime.Now > contract.ContractEndDate)
            {
                if (!isSuperAdmin && !hasRedirected)
                {
                    context.Response.Cookies.Append("HasRedirected", "true", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(1) // Cookie expires in 1 hour
                    });

                    context.Response.Redirect("/Home/ContractExpired");
                    return;
                }
            }

            await _next(context);
        }
    }
}
