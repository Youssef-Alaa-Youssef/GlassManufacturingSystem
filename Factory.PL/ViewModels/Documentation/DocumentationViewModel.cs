using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Documentation
{
    public class DocumentationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "YouTube Video URL")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string VideoUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}