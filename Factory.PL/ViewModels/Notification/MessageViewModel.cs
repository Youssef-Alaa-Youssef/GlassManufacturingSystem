//using Factory.DAL.Enums;
//using Factory.DAL.Models.Notifications;
//using Factory.PL.Services.Notification;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Windows.Input;

//namespace Factory.PL.ViewModels.Notification
//{
//    public class MessageViewModel : INotifyPropertyChanged
//    {
//        private readonly IMessageService _messageService;

//        public ObservableCollection<Message> Messages { get; set; }
//        public string NewMessageContent { get; set; }
//        public List<Attachment> PendingAttachments { get; set; }
//        public bool IsSending { get; set; }

//        public ICommand SendMessageCommand { get; private set; }
//        public ICommand AttachFileCommand { get; private set; }

//        public MessageViewModel(IMessageService messageService)
//        {
//            _messageService = messageService;
//            Messages = new ObservableCollection<Message>();
//            PendingAttachments = new List<Attachment>();

//            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
//            AttachFileCommand = new RelayCommand(AttachFile);

//            _messageService.OnMessageReceived += HandleNewMessage;
//            _messageService.OnMessageStatusChanged += HandleStatusChange;
//        }

//        private async void SendMessage()
//        {
//            if (string.IsNullOrWhiteSpace(NewMessageContent) && !PendingAttachments.Any())
//                return;

//            IsSending = true;

//            try
//            {
//                var message = new Message
//                {
//                    Content = NewMessageContent,
//                    Attachments = new List<Attachment>(PendingAttachments),
//                    SenderId = CurrentUser.Id,
//                    ReceiverId = ActiveChat.ContactId,
//                    Timestamp = DateTime.UtcNow,
//                    Status = MessageStatus.Queued
//                };

//                await _messageService.SendMessageAsync(message);

//                // Clear input
//                NewMessageContent = string.Empty;
//                PendingAttachments.Clear();
//            }
//            catch (Exception ex)
//            {
//                ErrorService.LogError(ex);
//                NotificationService.ShowError("Failed to send message. Please try again.");
//            }
//            finally
//            {
//                IsSending = false;
//            }
//        }

//        private bool CanSendMessage()
//        {
//            return !IsSending &&
//                   (!string.IsNullOrWhiteSpace(NewMessageContent) || PendingAttachments.Any());
//        }

//        private void AttachFile()
//        {
//            var selectedFiles = FilePickerService.PickMultipleFiles();

//            foreach (var file in selectedFiles)
//            {
//                var attachment = new Attachment
//                {
//                    FileName = file.Name,
//                    FileType = file.ContentType,
//                    FileSize = file.Size,
//                    // Upload to temp storage or prepare for upload
//                    FileUrl = FileUploadService.PrepareForUpload(file)
//                };

//                PendingAttachments.Add(attachment);
//            }
//        }

//        // Event handlers for real-time updates
//        private void HandleNewMessage(Message message)
//        {
//            if (message.SenderId == ActiveChat.ContactId ||
//                message.ReceiverId == ActiveChat.ContactId)
//            {
//                Messages.Add(message);
//            }
//        }

//        private void HandleStatusChange(string messageId, MessageStatus newStatus)
//        {
//            var message = Messages.FirstOrDefault(m => m.Id == messageId);
//            if (message != null)
//            {
//                message.Status = newStatus;
//            }
//        }

//        // INotifyPropertyChanged implementation
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
