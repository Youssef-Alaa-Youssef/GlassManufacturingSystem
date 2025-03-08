using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Home
{
    public class FAQSViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [StringLength(500, ErrorMessage = "Question cannot exceed 500 characters.")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Answer is required.")]
        [StringLength(2000, ErrorMessage = "Answer cannot exceed 2000 characters.")]
        public string Answer { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
