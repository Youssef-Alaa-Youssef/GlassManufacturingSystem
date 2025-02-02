var builder = WebApplication.CreateBuilder(args);

// Configure services
ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure middleware
MiddlewareConfiguration.ConfigureMiddleware(app, builder.Environment);

app.Run();