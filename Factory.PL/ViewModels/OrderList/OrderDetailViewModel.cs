namespace Factory.PL.ViewModels.OrderList
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string DeliveryStatus { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public double TotalSQM { get; set; }
        public double TotalLM { get; set; }
        public string JobNum { get; set; } = string.Empty;

    }
}
