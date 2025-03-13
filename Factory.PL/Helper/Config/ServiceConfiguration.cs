using Factory.BLL;
using Factory.BLL.InterFaces;
using Factory.DAL;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.Settings;
using Factory.PL.Helper;
using Factory.PL.Helper.Lang;
using Factory.PL.Services;
using Factory.PL.Services.Background;
using Factory.PL.Services.Dashboard;
using Factory.PL.Services.DataExportImport;
using Factory.PL.Services.Email;
using Factory.PL.Services.Localization;
using Factory.PL.Services.NavbarSettings;
using Factory.PL.Services.Order;
using Factory.PL.Services.Permissions;
using Factory.PL.Services.Permssions;
using Factory.PL.Services.Setting;
using Factory.PL.Services.UploadFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
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

        ConfigureLocalization(services);
        ConfigureApplicationServices(services, configuration);
        ConfigureDatabase(services, configuration);
        ConfigureIdentity(services);
        ConfigureSecurity(services);
    }

    private static void ConfigureLocalization(IServiceCollection services)
    {
        services.AddSingleton<IStringLocalizerFactory>(new JsonStringLocalizerFactory("Resources"));

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        var supportedCultures = new[]
        {
            new CultureInfo("en-US"), 
            new CultureInfo("ar-SA"), 
            new CultureInfo("fr-FR"), 
        };

        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization() 
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix); 
    }

    private static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton(new ExportImportSettings());
        services.AddScoped<IExportService, ExportService>();
        services.AddScoped<IImportService, ImportService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddHostedService<AccountDeletionBackgroundService>();
        services.Configure<EmailConfiguration>(configuration.GetSection("MailConfigurations"));
        services.AddSingleton<EmailConfiguration>();
        services.Configure<CompanyDetails>(configuration.GetSection("CompanyDetails"));
        services.AddSingleton<CompanyDetails>();
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ISettingsService, SettingsService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<IEmailService, EmailSender>();
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true; 
            options.Providers.Add<GzipCompressionProvider>(); 
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = System.IO.Compression.CompressionLevel.Optimal; 
        });

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