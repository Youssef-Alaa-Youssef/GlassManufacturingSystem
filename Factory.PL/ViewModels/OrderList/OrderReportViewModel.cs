namespace Factory.PL.ViewModels.OrderList
{
    public class OrderReportViewModel
    {
        public int TotalOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CancelledOrders { get; set; }
        public List<OrderDetailViewModel> Orders { get; set; } = new List<OrderDetailViewModel>();
        public List<int> MonthlyDeliveries { get; set; } = new List<int>();
        public List<double> TotalSQMData { get; set; } = new List<double>();
        public List<double> TotalLMData { get; set; } = new List<double>();
        public List<int> TotalItemsData { get; set; } = new List<int>();
        public Dictionary<string, int> OrderStatusDistribution { get; set; } = new Dictionary<string, int>(); // Order status distribution
        public Dictionary<string, int> TopCustomers { get; set; } = new Dictionary<string, int>(); // Top customers by total orders
    }

}