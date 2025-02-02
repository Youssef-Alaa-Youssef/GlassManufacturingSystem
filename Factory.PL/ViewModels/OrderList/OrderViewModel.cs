using Factory.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer Reference is required.")]
        public string CustomerReference { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project Name is required.")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Job No is required.")]
        public string JobNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required.")]
        public string Priority { get; set; } = string.Empty;

        public List<MachineType> SelectedMachines { get; set; } = new List<MachineType>();

        [Required(ErrorMessage = "Finished Date is required.")]
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; } = DateTime.Now.AddDays(10);

        [Required(ErrorMessage = "Signature is required.")]
        public string Signature { get; set; } = string.Empty;

        [Required(ErrorMessage = "Is Accepted is required.")]
        public bool IsAccepted { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}