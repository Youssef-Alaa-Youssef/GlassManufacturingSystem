using Factory.PL.Models;

namespace Factory.PL.Services.Order
{
    public class OrderService : IOrderService
    {
        public async Task<OrderData> GetOrderByIdAsync(string orderId)
        {
            await Task.Delay(10);

            return new OrderData
            {
                OrderId = orderId,
                InvoiceNumber = $"INV-{orderId}",
                OrderDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Paid",
                PaymentMethod = "Credit Card",

                Customer = new CustomerInfo
                {
                    Name = "John Doe",
                    Address = "123 Customer Street, City, Country",
                    Email = "john.doe@example.com",
                    Phone = "+1 234 567 890"
                },

                Items = new List<OrderItem>
                {
                    new OrderItem { Description = "Product A", Quantity = 2, UnitPrice = 45.00m },
                    new OrderItem { Description = "Product B", Quantity = 1, UnitPrice = 30.00m },
                },

                Subtotal = 120.00m,
                Tax = 10.00m,
                Shipping = 5.00m,
                Total = 135.00m,
                Notes = "All items are covered by a 30-day warranty. Returns accepted within 14 days of delivery."
            };
        }
    }
}
