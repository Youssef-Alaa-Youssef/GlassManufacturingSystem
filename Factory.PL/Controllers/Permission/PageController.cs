using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;
using Factory.PL.ViewModels.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Factory.Controllers
{
    public class PagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "Permission Management_Read")]
        public async Task<IActionResult> Index()
        {
            var pages = await _unitOfWork.GetRepository<Page>().GetAllAsync();
            return View(pages);
        }

        [Authorize(Policy = "Permission Management_Read")]
        public async Task<IActionResult> Details(int id)
        {
            var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        [Authorize(Policy = "Permission Management_Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "Permission Management_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel pageViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var page = new Page
                {
                    Name = pageViewModel.Name,
                    Action = pageViewModel.Action,
                    Controller = pageViewModel.Controller,
                    IsActive = pageViewModel.IsActive,
                    SubmoduleId = pageViewModel.SubmoduleId
                };

                try
                {
                    await _unitOfWork.GetRepository<Page>().AddAsync(page);
                    TempData["Success"] = "Page created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the page. Exception: {ex.Message}";
                }
            }

            return View(pageViewModel);
        }

        [Authorize(Policy = "Permission Management_Update")]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            var pageViewModel = new PageViewModel
            {
                Id = page.Id,
                Name = page.Name,
                Action = page.Action,
                Controller = page.Controller,
                IsActive = page.IsActive,
                SubmoduleId = page.SubmoduleId
            };

            return View(pageViewModel);
        }

        [Authorize(Policy = "Permission Management_Update")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PageViewModel pageViewModel)
        {
            if (id != pageViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);
                if (page == null)
                {
                    return NotFound();
                }

                page.Name = pageViewModel.Name;
                page.Action = pageViewModel.Action;
                page.Controller = pageViewModel.Controller;
                page.IsActive = pageViewModel.IsActive;
                page.SubmoduleId = pageViewModel.SubmoduleId;

                try
                {
                    await _unitOfWork.GetRepository<Page>().UpdateAsync(page);
                    TempData["Success"] = "Page updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the page. Exception: {ex.Message}";
                }
            }

            return View(pageViewModel);
        }

        [Authorize(Policy = "Permission Management_Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        [Authorize(Policy = "Permission Management_Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);
            if (page != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<Page>().RemoveAsync(page);
                    TempData["Success"] = "Page deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the page. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Page not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}