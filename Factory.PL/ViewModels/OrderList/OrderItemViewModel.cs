using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Width is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Width must be greater than zero.")]
        public decimal Width { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Height must be greater than zero.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public decimal SQM { get; set; }
        public decimal TotalSQM { get; set; }
        public decimal TotalLM { get; set; }

        [Required(ErrorMessage = "Customer Reference is required.")]
        public string CustomerReference { get; set; } = string.Empty;

        public int OrderId { get; set; }
    }
}
