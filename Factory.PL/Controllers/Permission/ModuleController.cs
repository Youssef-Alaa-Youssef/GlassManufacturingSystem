using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;
using Factory.PL.ViewModels.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Factory.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize()]
        public async Task<IActionResult> Index()
        {
            var modules = await _unitOfWork.GetRepository<Module>().GetAllAsync();
            return View(modules);
        }

        [Authorize()]
        public async Task<IActionResult> Details(int id)
        {
            var module = await _unitOfWork.GetRepository<Module>().GetByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        [Authorize()]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize()]

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ModulesViewModel ModulesViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var module = new Module
                {
                    Name = ModulesViewModel.Name,
                    Url = ModulesViewModel.Url,
                    IconClass = ModulesViewModel.IconClass,
                    //RolePermissions = ModulesViewModel.RolePermissions,
                    SubModules = ModulesViewModel.SubModules
                };

                try
                {
                    await _unitOfWork.GetRepository<Module>().AddAsync(module);
                    TempData["Success"] = "Module created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the module. Exception: {ex.Message}";
                }
            }

            return View(ModulesViewModel);
        }

        [Authorize()]
        public async Task<IActionResult> Edit(int id)
        {
            var module = await _unitOfWork.GetRepository<Module>().GetByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            var ModulesViewModel = new ModulesViewModel
            {
                Id = module.Id,
                Name = module.Name,
                Url = module.Url,
                IconClass = module.IconClass,
                //RolePermissions = module.RolePermissions,
                SubModules = module.SubModules
            };

            return View(ModulesViewModel);
        }
        [Authorize()]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModulesViewModel ModulesViewModel)
        {
            if (id != ModulesViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var module = await _unitOfWork.GetRepository<Module>().GetByIdAsync(id);
                if (module == null)
                {
                    return NotFound();
                }

                module.Name = ModulesViewModel.Name;
                module.Url = ModulesViewModel.Url;
                module.IconClass = ModulesViewModel.IconClass;
                //module.RolePermissions = ModulesViewModel.RolePermissions;
                module.SubModules = ModulesViewModel.SubModules;

                try
                {
                    await _unitOfWork.GetRepository<Module>().UpdateAsync(module);
                    TempData["Success"] = "Module updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the module. Exception: {ex.Message}";
                }
            }

            return View(ModulesViewModel);
        }

        [Authorize()]
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _unitOfWork.GetRepository<Module>().GetByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }
        [Authorize()]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _unitOfWork.GetRepository<Module>().GetByIdAsync(id);
            if (module != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<Module>().RemoveAsync(module);
                    TempData["Success"] = "Module deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the module. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Module not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}