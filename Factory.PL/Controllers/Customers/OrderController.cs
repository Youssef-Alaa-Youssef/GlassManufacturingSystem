//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using Factory.BLL.InterFaces;
//using Factory.DAL.Models.Customers;
//using Factory.PL.ViewModels.Customers;

//namespace Factory.Controllers
//{
//    [Authorize(Roles = "Administrator")]
//    public class OrdersController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public OrdersController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
//            return View(orders);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
//            if (order == null)
//            {
//                TempData["Error"] = "Order not found.";
//                return RedirectToAction(nameof(Index));
//            }

//            return View(order);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                var order = new Order
//                {
//                    CustomerId = orderViewModel.CustomerId,
//                    OrderDate = orderViewModel.OrderDate,
//                    GlassType = orderViewModel.GlassType,
//                    Height = orderViewModel.Height,
//                    Width = orderViewModel.Width,
//                    Quantity = orderViewModel.Quantity,
//                    TotalCost = orderViewModel.TotalCost,
//                    DeliveryMethod = orderViewModel.DeliveryMethod,
//                    DeliveryAddress = orderViewModel.DeliveryAddress,
//                };

//                try
//                {
//                    await _unitOfWork.GetRepository<Order>().AddAsync(order);
//                    TempData["Success"] = "Order has been successfully created!";
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = $"Failed to create the order. Error: {ex.Message}";
//                }
//            }

//            return View(orderViewModel);
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
//            if (order == null)
//            {
//                TempData["Error"] = "Order not found.";
//                return RedirectToAction(nameof(Index));
//            }

//            var orderViewModel = new OrderViewModel
//            {
//                CustomerId = order.CustomerId,
//                OrderDate = order.OrderDate,
//                GlassType = order.GlassType,
//                Height = order.Height,
//                Width = order.Width,
//                Quantity = order.Quantity,
//                TotalCost = order.TotalCost,
//                DeliveryMethod = order.DeliveryMethod,
//                DeliveryAddress = order.DeliveryAddress
//            };

//            return View(orderViewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, OrderViewModel orderViewModel)
//        {
//            if (id != orderViewModel.Id)
//            {
//                TempData["Error"] = "Order ID mismatch.";
//                return RedirectToAction(nameof(Index));
//            }

//            if (ModelState.IsValid)
//            {
//                var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
//                if (order == null)
//                {
//                    TempData["Error"] = "Order not found.";
//                    return RedirectToAction(nameof(Index));
//                }

//                order.CustomerId = orderViewModel.CustomerId;
//                order.OrderDate = orderViewModel.OrderDate;
//                order.GlassType = orderViewModel.GlassType;
//                order.Height = orderViewModel.Height;
//                order.Width = orderViewModel.Width;
//                order.Quantity = orderViewModel.Quantity;
//                order.TotalCost = orderViewModel.TotalCost;
//                order.DeliveryMethod = orderViewModel.DeliveryMethod;
//                order.DeliveryAddress = orderViewModel.DeliveryAddress;

//                try
//                {
//                    await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
//                    TempData["Success"] = "Order updated successfully!";
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = $"Failed to update the order. Error: {ex.Message}";
//                }
//            }

//            return View(orderViewModel);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
//            if (order == null)
//            {
//                TempData["Error"] = "Order not found.";
//                return RedirectToAction(nameof(Index));
//            }

//            return View(order);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
//            if (order != null)
//            {
//                try
//                {
//                    await _unitOfWork.GetRepository<Order>().RemoveAsync(order);
//                    TempData["Success"] = "Order deleted successfully!";
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = $"Failed to delete the order. Error: {ex.Message}";
//                }
//            }
//            else
//            {
//                TempData["Error"] = "Order not found.";
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
