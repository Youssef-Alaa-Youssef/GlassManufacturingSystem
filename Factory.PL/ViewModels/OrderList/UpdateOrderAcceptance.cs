using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OrderList
{
    public class UpdateOrderAcceptance
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public bool IsAccepted { get; set; }


    }
}

