using Factory.DAL.Models.Notifications;

namespace Factory.PL.Services.Notify
{
    public interface INotificationService
    {
        Task<List<Notification>> GetUnreadNotificationsAsync();
        Task MarkAsReadAsync(int notificationId);
        Task<int> GetUnreadNotificationCountAsync();
        Task SendNotificationAsync(string userId, string message, string type, string iconClass);
    }
}
