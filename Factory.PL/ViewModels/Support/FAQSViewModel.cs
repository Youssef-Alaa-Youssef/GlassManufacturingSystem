using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.ViewModels.Support
{
    public class FAQSViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Question { get; set; } = string.Empty;

        [Required]
        public string Answer { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
    }
}