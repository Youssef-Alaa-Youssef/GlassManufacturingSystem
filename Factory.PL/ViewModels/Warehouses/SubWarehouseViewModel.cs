using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Warehouses
{
    public class SubWarehouseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "English name is required.")]
        [StringLength(100, ErrorMessage = "English name cannot exceed 100 characters.")]
        [Display(Name = "English Name")]
        public string NameEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Arabic name is required.")]
        [StringLength(100, ErrorMessage = "Arabic name cannot exceed 100 characters.")]
        [Display(Name = "Arabic Name")]
        public string NameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "English address is required.")]
        [StringLength(200, ErrorMessage = "English address cannot exceed 200 characters.")]
        [Display(Name = "English Address")]
        public string AddressEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Arabic address is required.")]
        [StringLength(200, ErrorMessage = "Arabic address cannot exceed 200 characters.")]
        [Display(Name = "Arabic Address")]
        public string AddressAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Main Warehouse is required.")]
        [Display(Name = "Main Warehouse")]
        public int MainWarehouseId { get; set; }

        [Display(Name = "Available Main Warehouses")]
        public IEnumerable<SelectListItem> MainWarehouses { get; set; } = new List<SelectListItem>();
    }
}
