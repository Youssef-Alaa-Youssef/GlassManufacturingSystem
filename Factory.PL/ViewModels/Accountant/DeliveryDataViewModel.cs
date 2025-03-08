namespace Factory.PL.ViewModels.Accountant
{
    public class DeliveryDataViewModel
    {
        public int ItemId { get; set; }
        public bool IsDelivered { get; set; }
        public string DeliveryDate { get; set; } = string.Empty;
        public string DeliveredBy { get; set; } = string.Empty;
    }
}
