using Factory.DAL.Enums.Stores;

namespace Factory.PL.ViewModels.Warehouses
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public GlassProductType GlassType { get; set; }
    }
}
