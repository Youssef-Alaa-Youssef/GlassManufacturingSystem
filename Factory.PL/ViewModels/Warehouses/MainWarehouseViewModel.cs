using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Warehouses
{
    public class MainWarehouseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "English name is required.")]
        [StringLength(100, ErrorMessage = "English name cannot exceed 100 characters.")]
        [Display(Name = "Warehouse Name (EN)", Description = "Enter the name of the warehouse in English.")]
        public string NameEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Arabic name is required.")]
        [StringLength(100, ErrorMessage = "Arabic name cannot exceed 100 characters.")]
        [Display(Name = "Warehouse Name (AR)", Description = "Enter the name of the warehouse in Arabic.")]
        public string NameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "English address is required.")]
        [StringLength(200, ErrorMessage = "English address cannot exceed 200 characters.")]
        [Display(Name = "Warehouse Address (EN)", Description = "Enter the address of the warehouse in English.")]
        public string AddressEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Arabic address is required.")]
        [StringLength(200, ErrorMessage = "Arabic address cannot exceed 200 characters.")]
        [Display(Name = "Warehouse Address (AR)", Description = "Enter the address of the warehouse in Arabic.")]
        public string AddressAr { get; set; } = string.Empty;
    }
}
