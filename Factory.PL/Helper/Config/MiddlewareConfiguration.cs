using Factory.DAL.Configurations;
using Factory.DAL;
using Factory.PL.Helper;
using Factory.PL.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

public static class MiddlewareConfiguration
{
    public static void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
    {
        // Apply database migrations
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<FactDdContext>();
                dbContext.Database.MigrateAsync().Wait(); // Wait for migration to complete
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        // Configure error handling for production
        if (!env.IsDevelopment())
        {
            app.UseStatusCodePagesWithReExecute("/Home/ErrorProd");
            app.UseHsts();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;
                    context.Response.Redirect("/Home/ErrorProd");
                    await Task.CompletedTask;
                });
            });
        }

        // Middleware pipeline
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();

        // Localization
        var supportedCultures = new[] { "ar_EG", "en_US" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        // Use custom permission policy middleware (Before authentication)
        app.UseMiddleware<PermissionPolicyMiddleware>();

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Custom exception handling for production
        if (!env.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        // Default route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Seed initial data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DataSeeder.Initialize(services).Wait();
        }
    }
}
