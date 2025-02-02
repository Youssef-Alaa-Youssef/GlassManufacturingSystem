using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.DAL.Models.Warehouses
{
    public class SubWarehouse
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;

        public string AddressEn { get; set; } = string.Empty;

        public string AddressAr { get; set; } = string.Empty;

        public int MainWarehouseId { get; set; }

        [NotMapped]
        public virtual MainWarehouse? MainWarehouse { get; set; } = null;
    }
}
