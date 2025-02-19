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
        services.AddControllersWithViews();
        services.AddLocalization();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizationFactory>();
        services.AddMemoryCache();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("ar_EG"), new CultureInfo("en_US") };
            options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        services.Configure<EmailConfiguration>(configuration.GetSection("MailConfigurations"));
        services.AddSingleton<EmailConfiguration>();
        services.Configure<CompanyDetails>(configuration.GetSection("CompanyDetails"));
        services.AddSingleton<CompanyDetails>();
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        var factoryName = configuration["Factory:Name"];
        var domain = configuration["Factory:Domain"];
        var baseConnectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(factoryName) || string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(baseConnectionString))
        {
            throw new InvalidOperationException("Factory name, domain, or default connection string is not configured.");
        }

        var dynamicConnectionString = ConnectionStringBuilder.Build(baseConnectionString, factoryName, domain);

        services.AddDbContext<FactDdContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(dynamicConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
            }).EnableSensitiveDataLogging();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<INavigationService, NavigationService>();

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

        services.AddSession(options =>
        {
            options.Cookie.Name = "FactorySessionCookie";
            options.IdleTimeout = TimeSpan.FromDays(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
        });

        services.AddScoped<IEmailService, EmailSender>();

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

        //services.AddTransient<PermissionPolicyMiddleware>();
    }
}
