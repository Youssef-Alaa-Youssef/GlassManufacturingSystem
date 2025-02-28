using Factory.PL.Models;

namespace Factory.PL.Services.Order
{
    public interface IOrderService
    {
        Task<OrderData> GetOrderByIdAsync(string orderId);
    }
}
