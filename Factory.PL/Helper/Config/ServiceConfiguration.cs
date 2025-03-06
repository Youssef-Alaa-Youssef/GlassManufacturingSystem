using Factory.BLL;
using Factory.BLL.InterFaces;
using Factory.DAL;
using Factory.DAL.Models.Auth;
using Factory.PL.Helper;
using Factory.PL.Services;
using Factory.PL.Services.Background;
using Factory.PL.Services.Email;
using Factory.PL.Services.Localization;
using Factory.PL.Services.NavbarSettings;
using Factory.PL.Services.Order;
using Factory.PL.Services.Permssions;
using Factory.PL.Services.Setting;
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
        services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
            });

        var cultureInfo = new System.Globalization.CultureInfo("en-US");
        cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        ConfigureLocalization(services);
        ConfigureApplicationServices(services, configuration);
        ConfigureDatabase(services, configuration);
        ConfigureIdentity(services);
        ConfigureSecurity(services);
    }

    private static void ConfigureLocalization(IServiceCollection services)
    {
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.AddControllersWithViews();
        services.AddLocalization();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizationFactory>();
        services.AddMemoryCache();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("ar-EG"), new CultureInfo("en-US") };
            options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }

    private static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<AccountDeletionBackgroundService>();
        services.Configure<EmailConfiguration>(configuration.GetSection("MailConfigurations"));
        services.AddSingleton<EmailConfiguration>();
        services.Configure<CompanyDetails>(configuration.GetSection("CompanyDetails"));
        services.AddSingleton<CompanyDetails>();
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ISettingsService, SettingsService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<IEmailService, EmailSender>();
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
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
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }).EnableSensitiveDataLogging();
        });
    }

    private static void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<FactDdContext>()
            .AddDefaultTokenProviders();
    }

    private static void ConfigureSecurity(IServiceCollection services)
    {
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
    }
}