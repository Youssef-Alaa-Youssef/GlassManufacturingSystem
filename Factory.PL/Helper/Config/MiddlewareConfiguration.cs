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

        using (var scope = app.Services.CreateScope())
        {
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

        if (!env.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleware>();

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
        app.UseMiddleware<ContractExpirationMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();

        var supportedCultures = new[] { "ar_EG", "en_US" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);


        app.UseAuthentication();
        app.UseMiddleware<PermissionPolicyMiddleware>();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DataSeeder.Initialize(services).Wait();
        }
    }
}
