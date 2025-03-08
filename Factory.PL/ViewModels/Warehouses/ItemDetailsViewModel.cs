using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Warehouses
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }

        // Basic Information
        [Display(Name = "Item Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Item Type")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Color")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Thickness")]
        public string Thickness { get; set; } = string.Empty;

        [Display(Name = "Dimensions")]
        public string Dimensions { get; set; } = string.Empty;

        // Quantity and Pricing
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Value")]
        [DataType(DataType.Currency)]
        public decimal TotalValue => Quantity * UnitPrice;

        // Warehouse Information
        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        [Display(Name = "Sub-Warehouse")]
        public string SubWarehouseName { get; set; } = string.Empty;

        // Additional Properties
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; } = string.Empty;

        [Display(Name = "Manufacture Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ManufactureDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Is Fragile?")]
        public bool IsFragile { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; } = string.Empty;
    }
}