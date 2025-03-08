using Factory.BLL.InterFaces;
using Factory.DAL.Models.OrderList;
using Factory.PL.ViewModels.OrderList;
using Microsoft.AspNetCore.Mvc;

namespace Factory.PL.Controllers
{
    public class OrderReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
            var orderItems = await _unitOfWork.GetRepository<OrderItem>().GetAllAsync();

            var totalOrders = orders.Count();
            var deliveredOrders = orders.Count(o => o.Items.All(i => i.IsDelivered));
            var pendingOrders = orders.Count(o => o.Items.Any(i => !i.IsDelivered));
            var cancelledOrders = orders.Count(o => o.Status == "Cancelled");

            var orderDetails = orders.Select(o => new OrderDetailViewModel
            {
                OrderId = o.Id,
                CustomerName = o.CustomerName,
                OrderDate = o.Date,
                Status = o.Status,
                DeliveryStatus = o.Items.All(i => i.IsDelivered) ? "Delivered" : "Not Delivered",
                TotalItems = o.Items.Count,
                TotalSQM = o.TotalSQM,
                TotalLM = o.TotalLM,
                JobNum = o.JobNo,
            }).ToList();

            var monthlyDeliveries = new List<int> { 12, 19, 3, 5, 2, 3, 7, 8, 9, 10, 11, 12 }; // Replace with actual data
            var totalSQMData = orders.Select(o => o.TotalSQM).ToList(); // Total SQM per order
            var totalLMData = orders.Select(o => o.TotalLM).ToList(); // Total LM per order
            var totalItemsData = orders.Select(o => o.Items.Count).ToList(); // Total Items per order

            var orderStatusDistribution = new Dictionary<string, int>
        {
            { "Delivered", deliveredOrders },
            { "Pending", pendingOrders },
            { "Cancelled", cancelledOrders }
        };

            var topCustomers = orders
                .GroupBy(o => o.CustomerName)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .ToDictionary(g => g.Key, g => g.Count());

            var viewModel = new OrderReportViewModel
            {
                TotalOrders = totalOrders,
                DeliveredOrders = deliveredOrders,
                PendingOrders = pendingOrders,
                CancelledOrders = cancelledOrders,
                Orders = orderDetails,
                MonthlyDeliveries = monthlyDeliveries,
                TotalSQMData = totalSQMData,
                TotalLMData = totalLMData,
                TotalItemsData = totalItemsData,
                OrderStatusDistribution = orderStatusDistribution,
                TopCustomers = topCustomers,
            };

            return View(viewModel);
        }
    }
}
