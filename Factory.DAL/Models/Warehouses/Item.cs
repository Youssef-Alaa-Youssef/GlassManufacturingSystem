using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.DAL.Models.Warehouses
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CodeNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(250)]
        public string DescriptionEn { get; set; } = string.Empty;

        [MaxLength(250)]
        public string DescriptionAr { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public int MinimumStock { get; set; } = 10;

        public int CurrentStock { get; set; } = 0;

        public string UnitOfMeasure { get; set; } = "Piece";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public double Thickness { get; set; } = 4.0; 

        public double Width { get; set; } = 0; // in mm

        public double Height { get; set; } = 0; // in mm

        public string Color { get; set; } = "Clear";

        public string Quality { get; set; } = "Standard";

        public bool IsToughened { get; set; } = false;

        public bool IsLaminated { get; set; } = false;

        public int CategoryId { get; set; }

        public int MainWarehouseId { get; set; }

        public int? SubWarehouseId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [ForeignKey("MainWarehouseId")]
        public virtual MainWarehouse? MainWarehouse { get; set; }

        [ForeignKey("SubWarehouseId")]
        public virtual SubWarehouse? SubWarehouse { get; set; }

        public bool IsLowStock()
        {
            return CurrentStock <= MinimumStock;
        }

        public void UpdateStock(int quantity)
        {
            CurrentStock += quantity;
            UpdatedDate = DateTime.UtcNow;
        }
    }

}