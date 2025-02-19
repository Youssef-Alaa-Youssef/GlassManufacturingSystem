using Microsoft.AspNetCore.Mvc;
using Factory.BLL.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Factory.PL.ViewModels.OrderList;
using Factory.DAL.Models.Warehouses;
using Factory.DAL.Models.OrderList;

namespace Factory.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "Orders_Read")]
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
            var orderViewModels = orders.Select(MapToViewModel).ToList();
            return View(orderViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }
        private async Task<OrderViewModel> CreateOrderViewModel()
        {
            var lastOrder = (await _unitOfWork.GetRepository<Order>()
                                .GetAllAsync(order => order.JobNo != null))
                                .OrderByDescending(o => o.Id)
                                .FirstOrDefault();

            string newJobNumber = "1000";
            if (lastOrder != null)
            {
                newJobNumber = lastOrder.JobNo + 1; 
            }

            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync();

            return new OrderViewModel
            {
                JobNo = newJobNumber,
                Items = items.Select(i =>
                {
                    var dimensions = i.Dimensions?.Trim().Split('x', StringSplitOptions.RemoveEmptyEntries);
                    int width = 3210, height = 6000;

                    if (dimensions?.Length == 2 &&
                        int.TryParse(dimensions[0].Trim(), out int parsedWidth) &&
                        int.TryParse(dimensions[1].Trim(), out int parsedHeight))
                    {
                        width = parsedWidth;
                        height = parsedHeight;
                    }

                    return new OrderItemViewModel
                    {
                        Id = i.Id,
                        ItemName = i.Name,
                        Width = width,
                        Height = height
                    };
                }).ToList()
            };
        }

        [Authorize(Policy = "Orders_Create")]
        public async Task<IActionResult> Create()
        {
            var model = await CreateOrderViewModel();
            return View(model);
        }

        [Authorize(Policy = "Orders_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await CreateOrderViewModel();
                return View(model);
            }

            try
            {
                var order = MapToModel(orderViewModel);
                await _unitOfWork.GetRepository<Order>().AddAsync(order);
                TempData["Success"] = "Order created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.InnerException?.Message ?? ex.Message}";
                return View(orderViewModel);
            }
        }

        [Authorize(Policy = "Orders_Update")]

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }
        [Authorize(Policy = "Orders_Update")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderViewModel)
        {
            if (id != orderViewModel.Id || !ModelState.IsValid) return View(orderViewModel);

            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();

            try
            {
                MapToModel(orderViewModel, order);
                await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
                TempData["Success"] = "Order updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View(orderViewModel);
            }
        }

        [Authorize(Policy = "Orders_Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            return order == null ? NotFound() : View(order);
        }
        [Authorize(Policy = "Orders_Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order != null)
            {
                await _unitOfWork.GetRepository<Order>().RemoveAsync(order);
                TempData["Success"] = "Order deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Order not found.";
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Policy = "Orders_Read")]

        public async Task<IActionResult> Optimization(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var Items = await _unitOfWork.GetRepository<Item>().GetAllAsync();
            ViewBag.Item = Items;
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }

        private static OrderViewModel MapToViewModel(Order order) => new()
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerReference = order.CustomerReference,
            ProjectName = order.ProjectName,
            Date = order.Date,
            JobNo = order.JobNo,
            Address = order.Address,
            Priority = order.Priority,
            FinishDate = order.FinishDate,
            IsAccepted = order.IsAccepted,
            SelectedMachines = order.SelectedMachines,
            TotalSQM = order.TotalSQM,
            TotalLM = order.TotalLM,
            Items = order.Items.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity,
                CustomerReference = i.CustomerReference
            }).ToList()
        };

        private static Order MapToModel(OrderViewModel viewModel, Order? order = null)
        {
            order ??= new Order();
            order.CustomerName = viewModel.CustomerName;
            order.CustomerReference = viewModel.CustomerReference;
            order.ProjectName = viewModel.ProjectName;
            order.Date = viewModel.Date;
            order.JobNo = viewModel.JobNo;
            order.Address = viewModel.Address;
            order.Priority = viewModel.Priority;
            order.FinishDate = viewModel.FinishDate;
            order.IsAccepted = viewModel.IsAccepted;
            order.SelectedMachines = viewModel.SelectedMachines;
            order.TotalSQM = viewModel.TotalSQM;
            order.TotalLM = viewModel.TotalLM;

            order.Items = viewModel.Items.Select(i => new OrderItem
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity,
                CustomerReference = i.CustomerReference

            }).ToList();
            return order;
        }
    }
}