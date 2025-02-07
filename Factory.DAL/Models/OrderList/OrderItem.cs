namespace Factory.DAL.Models.OrderList
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public int Quantity { get; set; }
        public decimal SQM { get; set; }
        public decimal TotalSQM { get; set; }
        public decimal TotalLM { get; set; }
        public string CustomerReference { get; set; } = string.Empty;

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
