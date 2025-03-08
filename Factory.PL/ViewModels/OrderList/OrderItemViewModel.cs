using Factory.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Width is required")]
        [Range(1, 6000, ErrorMessage = "Width must be between 1 and 3200 mm")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(1, 6000, ErrorMessage = "Height must be between 1 and 5000 mm")]
        public double Height { get; set; }
        public double? Rank { get; set; }

        public double? StepWidth { get; set; }

        public double? StepHeight { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(InvariantDecimalModelBinder))]
        public double SQM { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(InvariantDecimalModelBinder))]
        public double TotalLM { get; set; }

        [Required(ErrorMessage = "Customer reference is required")]
        [StringLength(50, ErrorMessage = "Customer reference cannot exceed 50 characters")]
        public string CustomerReference { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(150, ErrorMessage = "Description cannot exceed 150 characters")]
        public string Description { get; set; } = string.Empty;
        public bool IsDelivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveredBy { get; set; } = string.Empty;

    }
}