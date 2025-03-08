using Factory.DAL.Enums;
using Factory.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer reference is required")]
        [StringLength(50, ErrorMessage = "Customer reference cannot exceed 50 characters")]
        public string CustomerReference { get; set; }

        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
        public string ProjectName { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string JobNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required]
        public string Priority { get; set; } = "High";

        [Required]
        public DateTime FinishDate { get; set; } = DateTime.Now.AddDays(10);

        public bool IsAccepted { get; set; } = true;

        public string Signature { get; set; }

        [Required(ErrorMessage = "At least one machine must be selected")]
        public List<MachineType> SelectedMachines { get; set; } = new List<MachineType>();

        //[Required]
        //[Range(0, double.MaxValue, ErrorMessage = "Total SQM must be a positive number")]
        [ModelBinder(BinderType = typeof(InvariantDecimalModelBinder))]
        public double TotalSQM { get; set; }

        //[Required]
        //[Range(0, double.MaxValue, ErrorMessage = "Total LM must be a positive number")]
        [ModelBinder(BinderType = typeof(InvariantDecimalModelBinder))]

        public double TotalLM { get; set; }
        public double? Rank { get; set; }
        public int? ItemCount { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}