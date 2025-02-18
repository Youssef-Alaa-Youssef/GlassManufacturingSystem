using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.PL.ViewModels.Warehouses
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        // Basic Information
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name must be between 1 and 100 characters.", MinimumLength = 1)]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "The Description must be less than 500 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Type field is required.")]
        [StringLength(50, ErrorMessage = "The Type must be between 1 and 50 characters.", MinimumLength = 1)]
        [Display(Name = "Item Type")]
        public string Type { get; set; } = string.Empty; // e.g., Glass Panel, Mirror, Tempered Glass

        [StringLength(50, ErrorMessage = "The Color must be less than 50 characters.")]
        [Display(Name = "Color")]
        public string Color { get; set; } = string.Empty; // e.g., Clear, Tinted, Frosted

        [Required(ErrorMessage = "The Thickness field is required.")]
        [StringLength(20, ErrorMessage = "The Thickness must be between 1 and 20 characters.", MinimumLength = 1)]
        [Display(Name = "Thickness")]
        public string Thickness { get; set; } = string.Empty; // e.g., 5mm, 10mm

        public string Dimensions { get; set; } = string.Empty;

        [Required(ErrorMessage = "The height (in mm) field is required.")]
        [Range(1, 5000, ErrorMessage = "The height must be between 1 and 5000 millimeters.")]
        [Display(Name = "Height (in mm)")]
        public int Height{ get; set; }

        [Required(ErrorMessage = "The width (in mm) field is required.")]
        [Range(1, 5000, ErrorMessage = "The width must be between 1 and 5000 millimeters.")]
        [Display(Name = "Width (in mm)")]
        public int Width { get; set; }

        public string width { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The Quantity must be a positive number.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Unit Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Unit Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; } 

        [Required(ErrorMessage = "The Warehouse field is required.")]
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; } 

        [Required(ErrorMessage = "The Sub-Warehouse field is required.")]
        [Display(Name = "Sub-Warehouse")]
        public int SubWarehouseId { get; set; } 

        public IEnumerable<SelectListItem> Warehouses { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SubWarehouses { get; set; } = new List<SelectListItem>();

        [StringLength(100, ErrorMessage = "The Manufacturer must be less than 100 characters.")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; } = string.Empty; 

        [Required(ErrorMessage = "The Manufacture Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Manufacture Date")]
        public DateTime ManufactureDate { get; set; } = DateTime.Now; 

        [Required(ErrorMessage = "The Expiry Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddYears(2); 

        [Display(Name = "Is Fragile?")]
        public bool IsFragile { get; set; }  = true;

        [StringLength(1000, ErrorMessage = "The Notes must be less than 1000 characters.")]
        [Display(Name = "Notes")]
        public string Notes { get; set; } = string.Empty; 
    }
}