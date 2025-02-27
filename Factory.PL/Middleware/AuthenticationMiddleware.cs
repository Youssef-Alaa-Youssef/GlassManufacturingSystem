namespace Factory.PL.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                _logger.LogWarning($"Unauthorized access attempt to {context.Request.Path}");
                context.Response.Redirect("/Auth/Login");
                return;
            }

            await _next(context);
        }
    }

}
