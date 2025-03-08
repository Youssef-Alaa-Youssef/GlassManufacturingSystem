namespace Factory.PL.ViewModels.OrderList
{
    public class LabelViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string JobNo { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public decimal Thickness { get; set; }
        public string OrderDate { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}