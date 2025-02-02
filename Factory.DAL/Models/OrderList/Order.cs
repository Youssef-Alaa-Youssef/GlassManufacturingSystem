namespace Factory.DAL.Models.OrderList
{
    public class Order
    {
        public int Id { get; set; } 
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerReference { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty ;
        public DateTime Date { get; set; } 
        public string JobNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime FinishDate { get; set; }
        public string Signature { get; set; } = string.Empty;
        public bool IsAccepted { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

}
