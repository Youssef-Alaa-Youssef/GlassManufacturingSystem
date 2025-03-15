using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels
{
    public class CountryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Code { get; set; } = string.Empty;

        [NotMapped]
        [ImageSizeValidation(300,200)]
        public IFormFile ImageFile { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}
