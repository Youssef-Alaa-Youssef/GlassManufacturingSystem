using Factory.DAL.Enums.Stores;

using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.Warehouses
{
    public class Category
    {
        public int Id { get; set; }

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

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public GlassProductType GlassType { get; set; } = GlassProductType.Standard;
        public int MainWarehouseId { get; set; }
        public virtual MainWarehouse MainWarehouse { get; set; }

        public virtual List<Item> Items { get; set; } = new List<Item>();

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }

}
