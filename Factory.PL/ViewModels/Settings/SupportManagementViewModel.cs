using Factory.DAL.Models.Support;

namespace Factory.DAL.ViewModels.Support
{
    public class SupportManagementViewModel
    {
        public IEnumerable<SupportTicket> Tickets { get; set; } = new List<SupportTicket>();

        public IEnumerable<FAQS> FAQs { get; set; } = new List<FAQS>();

        public int OpenTicketsCount { get; set; }

        public int ResolvedTicketsCount { get; set; }

        public int ArchivedFAQsCount { get; set; }

        public int ActiveFAQsCount { get; set; }
    }
}