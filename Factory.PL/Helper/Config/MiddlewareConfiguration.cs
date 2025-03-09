using Factory.DAL;
using Factory.DAL.Configurations;
using Factory.PL.Helper;
using Factory.PL.Middleware;
using Microsoft.EntityFrameworkCore;

public static class MiddlewareConfiguration
{
    public static void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
    {

        ApplyDatabaseMigrations(app);

        if (!env.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHsts();
        }

        app.UseMiddleware<ContractExpirationMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();

        ConfigureLocalization(app);

        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseMiddleware<Factory.PL.Middleware.PermissionPolicyMiddleware>();

        ConfigureRoutes(app);

        SeedDatabase(app);
        app.UseResponseCompression();
    }

    /// <summary>
    /// Applies pending migrations to the database.
    /// </summary>
    private static void ApplyDatabaseMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<FactDdContext>();
            dbContext.Database.MigrateAsync().Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }

    /// <summary>
    /// Configures supported cultures and localization settings.
    /// </summary>
    private static void ConfigureLocalization(WebApplication app)
    {
        var supportedCultures = new[] { "ar_EG", "en_US" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
    }

    /// <summary>
    /// Configures default routes.
    /// </summary>
    private static void ConfigureRoutes(WebApplication app)
    {
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }

    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    private static void SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        DataSeeder.Initialize(services).Wait();
    }
}
