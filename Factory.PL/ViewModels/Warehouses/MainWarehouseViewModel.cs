using Factory.DAL.Enums.Stores;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Warehouses
{
    public class MainWarehouseViewModel
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public int Capacity { get; set; }
        public WarehouseType Type { get; set; }
    }


}
