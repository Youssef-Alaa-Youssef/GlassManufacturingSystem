using Factory.DAL.Models.Support;

namespace Factory.DAL.ViewModels.Support
{
    public class SupportChatViewModel
    {
        public SupportTicket Ticket { get; set; } = new SupportTicket();

        public IEnumerable<SupportResponse> Responses { get; set; } = new List<SupportResponse>();

        public SupportResponseViewModel NewResponse { get; set; } = new SupportResponseViewModel();
    }
}