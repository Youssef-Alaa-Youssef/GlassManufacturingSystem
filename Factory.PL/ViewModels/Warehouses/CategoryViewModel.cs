using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.ViewModels.Warehouses
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name (English) is required.")]
        [MaxLength(100, ErrorMessage = "Name (English) cannot exceed 100 characters.")]
        public string NameEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name (Arabic) is required.")]
        [MaxLength(100, ErrorMessage = "Name (Arabic) cannot exceed 100 characters.")]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Description (English) cannot exceed 250 characters.")]
        public string DescriptionEn { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Description (Arabic) cannot exceed 250 characters.")]
        public string DescriptionAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Main Warehouse ID is required.")]
        public int MainWarehouseId { get; set; }

        // Additional properties for dropdowns or display purposes
        public string MainWarehouseName { get; set; } = string.Empty;
    }
}