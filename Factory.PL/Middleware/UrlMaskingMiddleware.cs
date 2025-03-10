using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;

public class SecureUrlMappingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SecureUrlMappingMiddleware> _logger;

    public SecureUrlMappingMiddleware(
        RequestDelegate next,
        ILogger<SecureUrlMappingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var path = context.Request.Path.Value?.ToLower();

        if (path != null && path.StartsWith("/s/"))
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var secureKey = path.Substring(3);

                    var pageRepository = unitOfWork.GetRepository<Page>();
                    var pages = await pageRepository.GetAllAsync();
                    var matchingPage = pages.FirstOrDefault(p =>
                        p.SecureUrlKey == secureKey && p.IsActive);

                    if (matchingPage != null)
                    {
                        context.Items["OriginalSecurePath"] = path;
                        context.Request.Path = $"/{matchingPage.Controller}/{matchingPage.Action}";
                        _logger.LogInformation(
                            $"Secure URL {path} mapped to {matchingPage.Controller}/{matchingPage.Action}");
                    }
                    else
                    {
                        _logger.LogWarning($"No matching page found for secure URL: {path}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing secure URL: {path}");
            }
        }

        await _next(context);
    }
}