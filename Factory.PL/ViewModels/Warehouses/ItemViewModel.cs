using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Warehouses
{

    public class ItemViewModel
    {
        public int Id { get; set; }
        public string CodeNumber { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
        public int CurrentStock { get; set; }
        public double Thickness { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Color { get; set; }
        public string Quality { get; set; }
        public bool IsToughened { get; set; }
        public bool IsLaminated { get; set; }
        public int CategoryId { get; set; }
        public int MainWarehouseId { get; set; }
    }

}