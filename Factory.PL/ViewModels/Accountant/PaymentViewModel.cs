using Factory.PL.ViewModels.OrderList;

namespace Factory.PL.ViewModels.Accountant
{
    public class PaymentViewModel
    {
        public IEnumerable<OrderViewModel> Order { get; set; }  
        public int OrderItemCount { get; set; }
    }

}
