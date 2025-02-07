using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.OrderList;
using Factory.BLL.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Factory.PL.ViewModels.OrderList;

namespace Factory.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
            var orderViewModels = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                CustomerReference = o.CustomerReference,
                ProjectName = o.ProjectName,
                Date = o.Date,
                JobNo = o.JobNo,
                Address = o.Address,
                Priority = o.Priority,
                FinishDate = o.FinishDate,
                IsAccepted = o.IsAccepted,
                Items = o.Items.Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    ItemName = i.ItemName,
                    Width = i.Width,
                    Height = i.Height,
                    Quantity = i.Quantity,
                    SQM = i.SQM,
                    TotalSQM = i.TotalSQM,
                    TotalLM = i.TotalLM,
                    CustomerReference = i.CustomerReference,
                    OrderId = i.OrderId
                }).ToList()
            }).ToList();

            return View(orderViewModels);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();

            var orderViewModel = new OrderViewModel
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
                Items = order.Items.Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    ItemName = i.ItemName,
                    Width = i.Width,
                    Height = i.Height,
                    Quantity = i.Quantity,
                    SQM = i.SQM,
                    TotalSQM = i.TotalSQM,
                    TotalLM = i.TotalLM,
                    CustomerReference = i.CustomerReference,
                    OrderId = i.OrderId
                }).ToList()
            };

            return View(orderViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerName = orderViewModel.CustomerName,
                    CustomerReference = orderViewModel.CustomerReference,
                    ProjectName = orderViewModel.ProjectName,
                    Date = orderViewModel.Date,
                    JobNo = orderViewModel.JobNo,
                    Address = orderViewModel.Address,
                    Priority = orderViewModel.Priority,
                    FinishDate = orderViewModel.FinishDate,
                    IsAccepted = orderViewModel.IsAccepted,
                    Items = orderViewModel.Items.Select(i => new OrderItem
                    {
                        ItemName = i.ItemName,
                        Width = i.Width,
                        Height = i.Height,
                        Quantity = i.Quantity,
                        SQM = i.SQM,
                        TotalSQM = i.TotalSQM,
                        TotalLM = i.TotalLM,
                        CustomerReference = i.CustomerReference
                    }).ToList()
                };

                try
                {
                    await _unitOfWork.GetRepository<Order>().AddAsync(order);
                    TempData["Success"] = "Order created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the order: {ex.Message}";
                }
            }

            return View(orderViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();

            var orderViewModel = new OrderViewModel
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
                IsAccepted = order.IsAccepted
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderViewModel)
        {
            if (id != orderViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
                if (order == null) return NotFound();

                order.CustomerName = orderViewModel.CustomerName;
                order.CustomerReference = orderViewModel.CustomerReference;
                order.ProjectName = orderViewModel.ProjectName;
                order.Date = orderViewModel.Date;
                order.JobNo = orderViewModel.JobNo;
                order.Address = orderViewModel.Address;
                order.Priority = orderViewModel.Priority;
                order.FinishDate = orderViewModel.FinishDate;
                order.IsAccepted = orderViewModel.IsAccepted;

                try
                {
                    await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
                    TempData["Success"] = "Order updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the order: {ex.Message}";
                }
            }

            return View(orderViewModel);
        }

        [Authorize(Policy = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
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
    }
}
