
namespace Factory.DAL.Models.Finance
{
    public class FinancialRecord
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}