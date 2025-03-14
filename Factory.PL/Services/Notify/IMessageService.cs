using Factory.DAL.Enums;
using Factory.DAL.Models.Settings;

namespace Factory.PL.Services.Notify
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(NotificationSettings message);
        Task<List<NotificationSettings>> GetMessageHistoryAsync(string chatId, int limit = 50, DateTime? before = null);
        Task<bool> ResendMessageAsync(string messageId);
        Task<bool> DeleteMessageAsync(string messageId);

        event Action<NotificationSettings> OnMessageReceived;
        event Action<string, NotificationFrequency> OnMessageStatusChanged;
    }
}
