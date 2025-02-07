using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer Reference is required.")]
        [StringLength(50, ErrorMessage = "Customer Reference cannot exceed 50 characters.")]
        public string CustomerReference { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(100, ErrorMessage = "Project Name cannot exceed 100 characters.")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Job Number is required.")]
        [StringLength(50, ErrorMessage = "Job Number cannot exceed 50 characters.")]
        public string JobNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required.")]
        [StringLength(20, ErrorMessage = "Priority cannot exceed 20 characters.")]
        public string Priority { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selected Machines are required.")]
        public string SelectedMachines { get; set; } = string.Empty;

        [Required(ErrorMessage = "Total SQM is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total SQM must be greater than 0.")]
        public decimal TotalSQM { get; set; }

        [Required(ErrorMessage = "Total LM is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total LM must be greater than 0.")]
        public decimal TotalLM { get; set; }

        [Required(ErrorMessage = "Finish Date is required.")]
        public DateTime FinishDate { get; set; }

        [Required(ErrorMessage = "Signature is required.")]
        [StringLength(100, ErrorMessage = "Signature cannot exceed 100 characters.")]
        public string Signature { get; set; } = string.Empty;

        [Required(ErrorMessage = "IsAccepted is required.")]
        public bool IsAccepted { get; set; }

        // List of OrderItems
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
