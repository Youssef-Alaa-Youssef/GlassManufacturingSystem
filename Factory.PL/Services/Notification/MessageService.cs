//using Factory.DAL.Enums;
//using Factory.DAL.Models.Notifications;
//using Newtonsoft.Json.Linq;

//namespace Factory.PL.Services.Notification
//{
//    public class MessageService : IMessageService
//    {
//        private readonly IApiClient _apiClient;
//        private readonly IWebSocketManager _socketManager;
//        private readonly IMessageRepository _localRepository;

//        public event Action<Message> OnMessageReceived;
//        public event Action<string, MessageStatus> OnMessageStatusChanged;

//        public MessageService(
//            IApiClient apiClient,
//            IWebSocketManager socketManager,
//            IMessageRepository localRepository)
//        {
//            _apiClient = apiClient;
//            _socketManager = socketManager;
//            _localRepository = localRepository;

//            _socketManager.Subscribe("message:new", HandleIncomingMessage);
//            _socketManager.Subscribe("message:status", HandleStatusUpdate);
//        }

//        event Action<Message> IMessageService.OnMessageReceived
//        {
//            add
//            {
//                throw new NotImplementedException();
//            }

//            remove
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public async Task<bool> SendMessageAsync(Message message)
//        {
//            await _localRepository.SaveMessageAsync(message);

//            try
//            {
//                if (message.Attachments?.Any() == true)
//                {
//                    foreach (var attachment in message.Attachments)
//                    {
//                        var uploadResult = await _apiClient.UploadAttachmentAsync(attachment);
//                        attachment.FileUrl = uploadResult.Url;
//                    }
//                }

//                var result = await _apiClient.PostAsync<MessageResponse>("messages/send", message);

//                if (result.Success)
//                {
//                    message.Id = result.MessageId;
//                    message.Status = MessageStatus.Sending;
//                    await _localRepository.UpdateMessageAsync(message);

//                    return true;
//                }
//                else
//                {
//                    message.Status = MessageStatus.Failed;
//                    await _localRepository.UpdateMessageAsync(message);
//                    return false;
//                }
//            }
//            catch (Exception ex)
//            {
//                message.Status = MessageStatus.Failed;
//                await _localRepository.UpdateMessageAsync(message);
//                throw;
//            }
//        }

//        public async Task<List<Message>> GetMessageHistoryAsync(string chatId, int limit = 50, DateTime? before = null)
//        {
//            var cachedMessages = await _localRepository.GetMessagesByChatIdAsync(chatId, limit, before);

//            if (NetworkService.IsConnected)
//            {
//                var serverMessages = await _apiClient.GetAsync<List<Message>>(
//                    $"messages?chatId={chatId}&limit={limit}&before={before}");

//                await _localRepository.MergeMessagesAsync(serverMessages);

//                return serverMessages;
//            }

//            return cachedMessages;
//        }

//        private void HandleIncomingMessage(JObject data)
//        {
//            var message = data.ToObject<Message>();

//            _localRepository.SaveMessageAsync(message).ConfigureAwait(false);

//            OnMessageReceived?.Invoke(message);
//        }

//        private void HandleStatusUpdate(JObject data)
//        {
//            string messageId = data["messageId"].ToString();
//            MessageStatus status = (MessageStatus)Enum.Parse(typeof(MessageStatus), data["status"].ToString());

//            _localRepository.UpdateMessageStatusAsync(messageId, status).ConfigureAwait(false);

//            OnMessageStatusChanged?.Invoke(messageId, status);
//        }

//         Task<List<Message>> IMessageService.GetMessageHistoryAsync(string chatId, int limit, DateTime? before)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> ResendMessageAsync(string messageId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> DeleteMessageAsync(string messageId)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
