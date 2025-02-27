using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Finance;
using Factory.BLL.InterFaces;
using Factory.PL.ViewModels.Finance;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
            var financialRecords = await _unitOfWork.GetRepository<FinancialRecord>().GetAllAsync();
            return View(financialRecords);
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
    }
}