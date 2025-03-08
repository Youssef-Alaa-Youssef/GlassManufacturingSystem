using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Home
{
    public class PartnerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Logo is required.")]
        [ImageSizeValidation(250, 190, ErrorMessage = "Each photo must be 250x190 pixels.")]

        public IFormFile? LogoFile { get; set; }
        public string? LogoUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
