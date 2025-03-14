using Factory.DAL.ViewModels.Warehouses;

namespace Factory.PL.ViewModels.Warehouses
{
    public class WarehouseViewModel
    {
        public List<MainWarehouseViewModel> MainWarehouses { get; set; } = new List<MainWarehouseViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}
