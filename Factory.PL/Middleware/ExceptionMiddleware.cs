namespace Factory.PL.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("AuthorizationPolicy"))
            {
                _logger.LogError(ex, "Authorization policy error occurred.");
                context.Response.Redirect("/Auth/AccessDenied");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access error occurred.");
                context.Response.Redirect("/Auth/AccessDenied");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
