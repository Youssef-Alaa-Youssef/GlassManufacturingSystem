using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Factory.DAL.Models
{
    public class Country
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
        public IFormFile ImageFile { get; set; } 

        public string ImagePath { get; set; } = string.Empty;
    }
}