namespace Factory.DAL.Models.Customers
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string GlassType { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Quantity { get; set; }
        public string EdgeFinish { get; set; } = string.Empty;
        public string AdditionalNotes { get; set; }

        public string ProductionStatus { get; set; }
        public DateTime? EstimatedCompletionDate { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }

        public decimal TotalCost { get; set; }
        public bool IsPaid { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
