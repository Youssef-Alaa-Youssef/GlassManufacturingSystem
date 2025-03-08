using Factory.BLL.InterFaces;
using Factory.DAL.Models.Documentation;
using Factory.PL.ViewModels.Documentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Factory.Controllers
{
    public class DocumentationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize()]

        public async Task<IActionResult> Index()
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetAllAsync();
            return View(documentation);
        }
        [Authorize()]
        public async Task<IActionResult> Help()
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetAllAsync();
            return View(documentation);
        }

        [Authorize()]

        public async Task<IActionResult> Details(int id)
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetByIdAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentationViewModel documentationViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var documentation = new Documentation
                {
                    Title = documentationViewModel.Title,
                    Description = documentationViewModel.Description,
                    Content = documentationViewModel.Content,
                    VideoUrl = documentationViewModel.VideoUrl,
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId ?? string.Empty
                };

                try
                {
                    await _unitOfWork.GetRepository<Documentation>().AddAsync(documentation);
                    TempData["Success"] = "Documentation created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the documentation. Exception: {ex.Message}";
                }
            }

            return View(documentationViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetByIdAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }

            var documentationViewModel = new DocumentationViewModel
            {
                Id = documentation.Id,
                Title = documentation.Title,
                Description = documentation.Description,
                Content = documentation.Content,
                VideoUrl = documentation.VideoUrl,
                CreatedAt = DateTime.UtcNow
            };

            return View(documentationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentationViewModel documentationViewModel)
        {
            if (id != documentationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var documentation = await _unitOfWork.GetRepository<Documentation>().GetByIdAsync(id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (documentation == null)
                {
                    return NotFound();
                }

                documentation.Title = documentationViewModel.Title;
                documentation.Description = documentationViewModel.Description;
                documentation.Content = documentationViewModel.Content;
                documentation.VideoUrl = documentationViewModel.VideoUrl;
                documentation.CreatedAt = DateTime.UtcNow;
                documentation.UserId = userId ?? string.Empty;

                try
                {
                    await _unitOfWork.GetRepository<Documentation>().UpdateAsync(documentation);
                    TempData["Success"] = "Documentation updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the documentation. Exception: {ex.Message}";
                }
            }

            return View(documentationViewModel);
        }
        [Authorize(Policy = "Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetByIdAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentation = await _unitOfWork.GetRepository<Documentation>().GetByIdAsync(id);
            if (documentation != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<Documentation>().RemoveAsync(documentation);
                    TempData["Success"] = "Documentation deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the documentation. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Documentation not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}