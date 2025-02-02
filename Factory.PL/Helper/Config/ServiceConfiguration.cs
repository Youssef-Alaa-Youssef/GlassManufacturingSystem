using Factory.BLL;
using Factory.BLL.InterFaces;
using Factory.DAL;
using Factory.PL.Helper;
using Factory.PL.Services;
using Factory.PL.Services.Email;
using Factory.PL.Services.Localization;
using Factory.PL.Services.NavbarSettings;
using Factory.PL.Services.Permssions;
using Factory.PL.Services.UploadFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add MVC, localization, and other configurations
        services.AddControllersWithViews();
        services.AddLocalization();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizationFactory>();

        // Configure localization options
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("ar_EG"), new CultureInfo("en_US") };
            options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        // Configure email, company details, and other app settings
        services.Configure<EmailConfiguration>(configuration.GetSection("MailConfigurations"));
        services.AddSingleton<EmailConfiguration>();
        services.Configure<CompanyDetails>(configuration.GetSection("CompanyDetails"));
        services.AddSingleton<CompanyDetails>();
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        // Dynamic connection string for the database
        var factoryName = configuration["Factory:Name"];
        var domain = configuration["Factory:Domain"];
        var baseConnectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(factoryName) || string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(baseConnectionString))
        {
            throw new InvalidOperationException("Factory name, domain, or default connection string is not configured.");
        }

        var dynamicConnectionString = ConnectionStringBuilder.Build(baseConnectionString, factoryName, domain);

        // Configure DbContext
        services.AddDbContext<FactDdContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(dynamicConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
            }).EnableSensitiveDataLogging();
        });

        // Register other services and repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<INavigationService, NavigationService>();

        // Identity and authentication configuration
        services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<FactDdContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "FactoryAuthCookie";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/LogOut";
            options.AccessDeniedPath = "/Auth/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(2);
        });

        // Session configuration
        services.AddSession(options =>
        {
            options.Cookie.Name = "FactorySessionCookie";
            options.IdleTimeout = TimeSpan.FromDays(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
        });

        // Email service registration
        services.AddScoped<IEmailService, EmailSender>();

        // Authorization and custom policy provider
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

        // Remove middleware registration here, it should be done in the pipeline
        // services.AddTransient<PermissionPolicyMiddleware>(); 
    }
}
