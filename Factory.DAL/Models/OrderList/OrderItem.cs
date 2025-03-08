namespace Factory.DAL.Models.OrderList
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public double? StepWidth { get; set; }
        public double? StepHeight { get; set; }
        public int Quantity { get; set; }
        public double SQM { get; set; }
        public double TotalLM { get; set; }
        public string CustomerReference { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? Rank { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveredBy { get; set; } = string.Empty;

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}