using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderItemViewModel
    {
        [Required(ErrorMessage = "Item Name is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Width is required.")]
        public decimal Width { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        public decimal Height { get; set; }

        public decimal StepWidth { get; set; }
        public decimal StepHeight { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        public decimal SQM { get; set; }
        public decimal TotalSQM { get; set; }
        public decimal TotalLM { get; set; }

        [Required(ErrorMessage = "Customer Reference is required.")]
        public string CustomerReference { get; set; } = string.Empty;
    }
}
