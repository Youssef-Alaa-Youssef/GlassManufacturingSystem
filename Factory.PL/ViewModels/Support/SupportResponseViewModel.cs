using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.ViewModels.Support
{
    public class SupportResponseViewModel
    {
        [Required]
        public string ResponseText { get; set; } = string.Empty;

        [Required]
        public int SupportTicketId { get; set; }
    }
}