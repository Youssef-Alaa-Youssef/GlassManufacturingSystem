using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class OrderRankUpdateModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public double Rank { get; set; }

    }
}
