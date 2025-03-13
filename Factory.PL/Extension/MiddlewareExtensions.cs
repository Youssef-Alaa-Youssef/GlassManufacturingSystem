public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
    {
        var localizationOptions = app.ApplicationServices.GetRequiredService<RequestLocalizationOptions>();
        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}