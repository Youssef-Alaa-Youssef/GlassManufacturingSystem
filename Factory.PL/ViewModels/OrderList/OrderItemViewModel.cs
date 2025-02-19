using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        [StringLength(200, ErrorMessage = "Item Name cannot exceed 200 characters.")]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Width is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Width must be greater than 0.")]
        public decimal Width { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Step Width must be greater than 0.")]
        public decimal StepWidth { get; set; }


        [Required(ErrorMessage = "Height is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public decimal Height { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Step Height must be greater than 0.")]
        public decimal StepHeight { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "SQM is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid value for SQM.")]
        public decimal SQM { get; set; }

        [Required(ErrorMessage = "Total SQM is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid value for TotalLM.")]
        public decimal TotalLM { get; set; }

        [Required(ErrorMessage = "Total LM is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid value for TotalSQM.")]
        public decimal TotalSQM { get; set; }

        [Required(ErrorMessage = "Customer Reference is required.")]
        [StringLength(100, ErrorMessage = "Customer Reference cannot exceed 100 characters.")]
        public string CustomerReference { get; set; } = string.Empty;

        public int? OrderId { get; set; }
    }
}
