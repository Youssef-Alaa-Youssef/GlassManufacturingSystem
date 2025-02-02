
namespace Factory.DAL.Models.Warehouses
{
    public class MainWarehouse
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;

        public string AddressEn { get; set; } = string.Empty;

        public string AddressAr { get; set; } = string.Empty;

        public virtual List<SubWarehouse> SubWarehouses { get; set; } = new List<SubWarehouse>();
    }

}

