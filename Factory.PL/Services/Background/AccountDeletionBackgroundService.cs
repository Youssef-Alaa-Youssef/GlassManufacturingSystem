using Factory.DAL.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Factory.PL.Services.Background
{
    public class AccountDeletionBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AccountDeletionBackgroundService> _logger;

        public AccountDeletionBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<AccountDeletionBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                        var usersToDelete = await userManager.Users
                            .Where(u => u.DeleteRequestedOn != null && u.DeleteRequestedOn.Value.AddDays(10) <= DateTime.UtcNow)
                            .ToListAsync<ApplicationUser>(stoppingToken);

                        foreach (var user in usersToDelete)
                        {
                            var result = await userManager.DeleteAsync(user);
                            if (result.Succeeded)
                            {
                                _logger.LogInformation("Deleted user {UserId} ({Email})", user.Id, user.Email);
                            }
                            else
                            {
                                _logger.LogError("Failed to delete user {UserId}: {Errors}", user.Id, string.Join(", ", result.Errors));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting users.");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
