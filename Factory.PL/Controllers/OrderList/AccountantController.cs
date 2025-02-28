using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Finance;
using Factory.BLL.InterFaces;
using Factory.PL.ViewModels.Finance;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Factory.DAL.Models.OrderList;
using Factory.PL.ViewModels.OrderList;

namespace Factory.Controllers
{
    [Authorize]
    public class AccountantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountantController(IUnitOfWork unitOfWork)
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
            var financialRecord = await _unitOfWork.GetRepository<FinancialRecord>().GetByIdAsync(id);
            if (financialRecord == null)
            {
                return NotFound();
            }

            return View(financialRecord);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinancialRecordViewModel financialRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var financialRecord = new FinancialRecord
                {
                    Description = financialRecordViewModel.Description,
                    Amount = financialRecordViewModel.Amount,
                    TransactionType = financialRecordViewModel.TransactionType,
                    Date = DateTime.UtcNow,
                    UserId = userId ?? string.Empty
                };

                try
                {
                    await _unitOfWork.GetRepository<FinancialRecord>().AddAsync(financialRecord);
                    TempData["Success"] = "Financial record created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the financial record. Exception: {ex.Message}";
                }
            }

            return View(financialRecordViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var financialRecord = await _unitOfWork.GetRepository<FinancialRecord>().GetByIdAsync(id);
            if (financialRecord == null)
            {
                return NotFound();
            }

            var financialRecordViewModel = new FinancialRecordViewModel
            {
                Id = financialRecord.Id,
                Description = financialRecord.Description,
                Amount = financialRecord.Amount,
                TransactionType = financialRecord.TransactionType,
                Date = financialRecord.Date
            };

            return View(financialRecordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FinancialRecordViewModel financialRecordViewModel)
        {
            if (id != financialRecordViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var financialRecord = await _unitOfWork.GetRepository<FinancialRecord>().GetByIdAsync(id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (financialRecord == null)
                {
                    return NotFound();
                }

                financialRecord.Description = financialRecordViewModel.Description;
                financialRecord.Amount = financialRecordViewModel.Amount;
                financialRecord.TransactionType = financialRecordViewModel.TransactionType;
                financialRecord.Date = DateTime.UtcNow;
                financialRecord.UserId = userId ?? string.Empty;

                try
                {
                    await _unitOfWork.GetRepository<FinancialRecord>().UpdateAsync(financialRecord);
                    TempData["Success"] = "Financial record updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the financial record. Exception: {ex.Message}";
                }
            }

            return View(financialRecordViewModel);
        }

        [Authorize(Policy = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var financialRecord = await _unitOfWork.GetRepository<FinancialRecord>().GetByIdAsync(id);
            if (financialRecord == null)
            {
                return NotFound();
            }

            return View(financialRecord);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialRecord = await _unitOfWork.GetRepository<FinancialRecord>().GetByIdAsync(id);
            if (financialRecord != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<FinancialRecord>().RemoveAsync(financialRecord);
                    TempData["Success"] = "Financial record deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the financial record. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Financial record not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateReport()
        {
            var financialRecords = await _unitOfWork.GetRepository<FinancialRecord>().GetAllAsync();
            var report = financialRecords
                .GroupBy(f => f.TransactionType)
                .Select(g => new FinancialReportViewModel
                {
                    TransactionType = g.Key,
                    TotalAmount = g.Sum(f => f.Amount)
                })
                .ToList();

            return View(report);
        }

        public async Task<IActionResult> Payment(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateOrderRank([FromBody] OrderRankUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input data" });
            }

            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.GetByIdAsync(model.OrderId);

                if (order == null)
                {
                    return NotFound(new { success = false, message = "Order not found" });
                }

                if (model.Rank < 0)
                {
                    return BadRequest(new { success = false, message = "Invalid rank value" });
                }

                order.Rank = model.Rank;
                await orderRepository.UpdateAsync(order);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order rank: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while updating rank" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderAcceptance([FromBody] UpdateOrderAcceptance model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input data" });
            }

            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.GetByIdAsync(model.OrderId);

                if (order == null)
                {
                    return NotFound(new { success = false, message = "Order not found" });
                }
                order.IsAccepted = model.IsAccepted;
                await orderRepository.UpdateAsync(order);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order rank: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while updating rank" });
            }
        }

        public static OrderViewModel MapToViewModel(Order order) => new()
        {
            Rank = order.Rank,
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
            Items = order.Items?.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity,
                CustomerReference = i.CustomerReference,
                Description = i.Description
            }).ToList() ?? new List<OrderItemViewModel>()
        };
    }
}