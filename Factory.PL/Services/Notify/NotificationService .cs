using Microsoft.AspNetCore.SignalR;
using Factory.BLL.InterFaces;
using Factory.DAL.Models.Notifications;
using Factory.PL.Hubs;
using Factory.PL.Services.Notify;
using Microsoft.EntityFrameworkCore;

namespace Factory.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(
            IUnitOfWork unitOfWork,
            IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync()
        {
            return  _unitOfWork.GetRepository<Notification>()
                                    .Query()
                                    .Where(n => !n.IsRead)
                                    .OrderByDescending(n => n.CreatedAt)
                                    .ToList();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _unitOfWork.GetRepository<Notification>().GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _unitOfWork.GetRepository<Notification>().UpdateAsync(notification);
            }
        }

        public async Task<int> GetUnreadNotificationCountAsync()
        {
            return await _unitOfWork.GetRepository<Notification>().Query()
                .CountAsync(n => !n.IsRead);
        }

        public async Task SendNotificationAsync(string userId, string message, string type, string iconClass)
        {
            var notification = new Notification
            {
                Message = message,
                Type = type,
                IconClass = iconClass,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _unitOfWork.GetRepository<Notification>().AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                notification.Id,
                notification.Message,
                notification.CreatedAt,
                notification.Type,
                notification.IconClass
            });
        }
    }
}