using Factory.PL.Middleware;

namespace Factory.PL.Extension
{

    public static class SecureUrlMappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecureUrlMapping(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecureUrlMappingMiddleware>();
        }
    }
}
