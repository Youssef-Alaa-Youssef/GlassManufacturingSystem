using Factory.DAL.Enums.Stores;
using System.ComponentModel.DataAnnotations;
namespace Factory.DAL.Models.Warehouses
{
    public class MainWarehouse
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NameAr { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string AddressEn { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string AddressAr { get; set; } = string.Empty;

        public int Capacity { get; set; } // Maximum capacity of the warehouse

        public int CurrentStock { get; set; } // Current stock in the warehouse

        public bool IsActive { get; set; } = true;

        public WarehouseType Type { get; set; }

        public WarehouseStatus Status { get; set; } = WarehouseStatus.Operational;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(100)]
        public string Manager { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public virtual List<Category> Categories { get; set; } = new List<Category>();

        public virtual List<SubWarehouse> SubWarehouses { get; set; } = new List<SubWarehouse>();

        public void AddSubWarehouse(SubWarehouse subWarehouse)
        {
            SubWarehouses.Add(subWarehouse);
        }

        public void RemoveSubWarehouse(SubWarehouse subWarehouse)
        {
            SubWarehouses.Remove(subWarehouse);
        }

        public bool CheckCapacity(int additionalStock)
        {
            return CurrentStock + additionalStock <= Capacity;
        }

        public void UpdateStock(int stockChange)
        {
            if (CheckCapacity(stockChange))
            {
                CurrentStock += stockChange;
            }
            else
            {
                throw new InvalidOperationException("Exceeds warehouse capacity.");
            }
        }
    }
}