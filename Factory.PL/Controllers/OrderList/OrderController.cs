using Microsoft.AspNetCore.Mvc;
using Factory.BLL.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Factory.PL.ViewModels.OrderList;
using Factory.DAL.Models.Warehouses;
using Factory.DAL.Models.OrderList;

namespace Factory.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        public async Task<IActionResult> Create()
        {
            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync();
            var model = new OrderViewModel
            {
                Items = items.Select(i => new OrderItemViewModel { Id = i.Id, ItemName = i.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) return View(orderViewModel);

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

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }

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

        [Authorize(Policy = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            return order == null ? NotFound() : View(order);
        }

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

        public async Task<IActionResult> Optimization(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
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
            Items = order.Items.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity
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
            order.Items = viewModel.Items.Select(i => new OrderItem
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity
            }).ToList();
            return order;
        }
    }
}