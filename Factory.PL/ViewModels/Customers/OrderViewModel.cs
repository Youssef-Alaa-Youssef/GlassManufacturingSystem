using System.ComponentModel.DataAnnotations;
namespace Factory.PL.ViewModels.Customers
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer selection is mandatory. Please choose a customer.")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Please specify the date of the order.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Glass type is required. Please provide the type of glass.")]
        [MaxLength(50, ErrorMessage = "Glass type cannot exceed 50 characters.")]
        public string GlassType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Height is required. Please provide the height of the glass.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Height must be a positive number.")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Width is required. Please provide the width of the glass.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Width must be a positive number.")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Please specify the quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Total cost is required. Please calculate and provide the total cost.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total cost must be greater than zero.")]
        public decimal TotalCost { get; set; }

        [MaxLength(100, ErrorMessage = "Delivery method description cannot exceed 100 characters.")]
        public string DeliveryMethod { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Delivery address cannot exceed 250 characters.")]
        public string DeliveryAddress { get; set; } = string.Empty;
    }
}
