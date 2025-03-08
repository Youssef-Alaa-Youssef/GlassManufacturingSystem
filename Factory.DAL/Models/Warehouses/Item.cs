namespace Factory.DAL.Models.Warehouses
{
    public class Item
    {
        public int Id { get; set; }

        // Basic Information
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Thickness { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;

        // Quantity and Pricing
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalValue => Quantity * UnitPrice;

        // Warehouse Information
        public int WarehouseId { get; set; }
        public virtual MainWarehouse Warehouse { get; set; }

        // Sub-Warehouse Information
        public int SubWarehouseId { get; set; }
        public virtual SubWarehouse SubWarehouse { get; set; }

        // Additional Properties
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsFragile { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}