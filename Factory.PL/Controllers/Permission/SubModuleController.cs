using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;
using Factory.PL.ViewModels.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    public class SubModuleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubModuleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize()]
        public async Task<IActionResult> Index()
        {
            var modules = (await _unitOfWork.GetRepository<Module>()
            .GetAllAsync())
            .Select(m => new
            {
               m.Id,
               m.Name,
               m.IconClass
            })
            .ToList();

            ViewBag.Modules = modules;
            var subModules = await _unitOfWork.GetRepository<SubModule>().GetAllAsync();
            return View(subModules);
        }

        [Authorize()]
        public async Task<IActionResult> Details(int id)
        {
            var subModule = await _unitOfWork.GetRepository<SubModule>().GetByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }

            return View(subModule);
        }

        [Authorize()]
        public async Task<IActionResult> Create()
        {
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            var viewModel = new SubModulesViewModel
            {
                Modules = ConvertToSelectList(modules) // Convert modules to SelectListItem
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> Create(SubModulesViewModel SubModulesViewModel)
        {
            if (ModelState.IsValid)
            {
                var subModule = new SubModule
                {
                    Name = SubModulesViewModel.Name,
                    Controller = SubModulesViewModel.Controller,
                    Action = SubModulesViewModel.Action,
                    Title = SubModulesViewModel.Title,
                    ModuleId = SubModulesViewModel.ModuleId
                };

                try
                {
                    await _unitOfWork.GetRepository<SubModule>().AddAsync(subModule);
                    TempData["Success"] = "SubModule created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the SubModule. Exception: {ex.Message}";
                }
            }

            // If ModelState is invalid, reload the modules dropdown
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            SubModulesViewModel.Modules = ConvertToSelectList(modules); // Convert modules to SelectListItem

            return View(SubModulesViewModel);
        }

        [Authorize()]
        public async Task<IActionResult> Edit(int id)
        {
            var subModule = await _unitOfWork.GetRepository<SubModule>().GetByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }

            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            var SubModulesViewModel = new SubModulesViewModel
            {
                Id = subModule.Id,
                Name = subModule.Name,
                Controller = subModule.Controller,
                Action = subModule.Action,
                Title = subModule.Title,
                ModuleId = subModule.ModuleId,
                Modules = ConvertToSelectList(modules) // Convert modules to SelectListItem
            };

            return View(SubModulesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> Edit(int id, SubModulesViewModel SubModulesViewModel)
        {
            if (id != SubModulesViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subModule = await _unitOfWork.GetRepository<SubModule>().GetByIdAsync(id);
                if (subModule == null)
                {
                    return NotFound();
                }

                subModule.Name = SubModulesViewModel.Name;
                subModule.Controller = SubModulesViewModel.Controller;
                subModule.Action = SubModulesViewModel.Action;
                subModule.Title = SubModulesViewModel.Title;
                subModule.ModuleId = SubModulesViewModel.ModuleId;

                try
                {
                    await _unitOfWork.GetRepository<SubModule>().UpdateAsync(subModule);
                    TempData["Success"] = "SubModule updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the SubModule. Exception: {ex.Message}";
                }
            }

            // If ModelState is invalid, reload the modules dropdown
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            SubModulesViewModel.Modules = ConvertToSelectList(modules); // Convert modules to SelectListItem

            return View(SubModulesViewModel);
        }

        [Authorize()]
        public async Task<IActionResult> Delete(int id)
        {
            var subModule = await _unitOfWork.GetRepository<SubModule>().GetByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }

            return View(subModule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subModule = await _unitOfWork.GetRepository<SubModule>().GetByIdAsync(id);
            if (subModule != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<SubModule>().RemoveAsync(subModule);
                    TempData["Success"] = "SubModule deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the SubModule. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "SubModule not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetSubModulesByModule(int moduleId)
        {
            var subModules = await _unitOfWork.GetRepository<SubModule>()
                .Query() 
                .Where(s => s.ModuleId == moduleId)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    controller = s.Controller,
                    action = s.Action,
                    title = s.Title,
                    module = new { name = s.Module.Name }
                })
                .ToListAsync(); 
            return Json(subModules);
        }
        private IEnumerable<SelectListItem> ConvertToSelectList(IEnumerable<Module> modules)
        {
            return modules.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
        }
    }
}