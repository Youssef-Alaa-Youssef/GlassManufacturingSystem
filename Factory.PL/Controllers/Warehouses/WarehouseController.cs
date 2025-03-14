using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Warehouses;
using Factory.DAL.ViewModels.Warehouses;
using Factory.PL.ViewModels.Warehouses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.PL.Controllers.Warehouses
{
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public class WarehouseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch categories from the database
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            ViewBag.Categories = categories; // Assign as a list, not a SelectList

            // Fetch main warehouses (if needed)
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync(includeProperties: "SubWarehouses");
            return View(mainWarehouses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id, includeProperties: "SubWarehouses");
            if (mainWarehouse == null)
            {
                return NotFound();
            }
            return View(mainWarehouse);
        }

        public IActionResult Create()
        {
            return View(new MainWarehouse());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainWarehouse model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<MainWarehouse>().AddAsync(model);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Main warehouse created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Invalid data. Please check your inputs.";
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
            if (mainWarehouse == null)
            {
                return NotFound();
            }
            return View(mainWarehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MainWarehouse model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<MainWarehouse>().UpdateAsync(model);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Main warehouse updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Invalid data. Please check your inputs.";
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>()
                    .Query()
                    .Include(w => w.SubWarehouses) 
                    .FirstOrDefaultAsync(w => w.Id == id);

                if (mainWarehouse == null)
                {
                    return Json(new { success = false, message = "Main warehouse not found." });
                }

                if (mainWarehouse.SubWarehouses != null && mainWarehouse.SubWarehouses.Any())
                {
                    return Json(new { success = false, message = "Cannot delete the warehouse because it has associated sub-stores." });
                }

                await _unitOfWork.GetRepository<MainWarehouse>().RemoveAsync(mainWarehouse);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Main warehouse deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the warehouse." });
            }
        }
        public async Task<IActionResult> ManageSubWarehouses(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id, includeProperties: "SubWarehouses");
            if (mainWarehouse == null)
            {
                return NotFound();
            }
            return View(mainWarehouse);
        }

        public async Task<IActionResult> CreateSubWarehouse()
        {
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var model = new SubWarehouseViewModel
            {
                MainWarehouses = mainWarehouses.Select(mw => new SelectListItem
                {
                    Value = mw.Id.ToString(),
                    Text = mw.NameEn
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubWarehouse(SubWarehouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subWarehouse = new SubWarehouse
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    AddressEn = model.AddressEn,
                    AddressAr = model.AddressAr,
                    MainWarehouseId = model.MainWarehouseId
                };

                await _unitOfWork.GetRepository<SubWarehouse>().AddAsync(subWarehouse);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Sub-warehouse created successfully!";
                return RedirectToAction(nameof(Index));
            }

            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            model.MainWarehouses = mainWarehouses.Select(mw => new SelectListItem
            {
                Value = mw.Id.ToString(),
                Text = mw.NameEn
            });

            TempData["Error"] = "Invalid data. Please check your inputs.";
            return View(model);
        }

        public async Task<IActionResult> EditSubWarehouse(int id)
        {
            var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
            if (subWarehouse == null)
            {
                return NotFound();
            }

            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var model = new SubWarehouseViewModel
            {
                Id = subWarehouse.Id,
                NameEn = subWarehouse.NameEn,
                NameAr = subWarehouse.NameAr,
                AddressEn = subWarehouse.AddressEn,
                AddressAr = subWarehouse.AddressAr,
                MainWarehouseId = subWarehouse.MainWarehouseId,
                MainWarehouses = mainWarehouses.Select(mw => new SelectListItem
                {
                    Value = mw.Id.ToString(),
                    Text = mw.NameEn
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubWarehouse(int id, SubWarehouseViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subWarehouse = new SubWarehouse
                {
                    Id = model.Id,
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    AddressEn = model.AddressEn,
                    AddressAr = model.AddressAr,
                    MainWarehouseId = model.MainWarehouseId
                };

                await _unitOfWork.GetRepository<SubWarehouse>().UpdateAsync(subWarehouse);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Sub-warehouse updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            model.MainWarehouses = mainWarehouses.Select(mw => new SelectListItem
            {
                Value = mw.Id.ToString(),
                Text = mw.NameEn
            });

            TempData["Error"] = "Invalid data. Please check your inputs.";
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubWarehouseConfirmed(int id)
        {
            try
            {
                var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
                if (subWarehouse == null)
                {
                    return Json(new { success = false, message = "Sub-warehouse not found." });
                }

                await _unitOfWork.GetRepository<SubWarehouse>().RemoveAsync(subWarehouse);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Sub-warehouse deleted successfully!" });
            }
            catch (Exception )
            {
                return Json(new { success = false, message = "An error occurred while deleting the sub-warehouse." });
            }
        }

        public async Task<IActionResult> WarehouseMangment()
        {
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync();

            var viewModel = new WarehouseViewModel
            {
                MainWarehouses = mainWarehouses.Select(mw => new MainWarehouseViewModel
                {
                    Id = mw.Id,
                    NameEn = mw.NameEn,
                    NameAr = mw.NameAr
                }).ToList(),
                Categories = categories.Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameEn = c.NameEn,
                    NameAr = c.NameAr
                }).ToList(),
                Items = items.Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    NameEn = i.NameEn,
                    NameAr = i.NameAr
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddMainWarehouse(MainWarehouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mainWarehouse = new MainWarehouse
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    AddressEn = model.AddressEn,
                    AddressAr = model.AddressAr,
                    Capacity = model.Capacity,
                    Type = model.Type,
                    IsActive = true
                };

                await _unitOfWork.GetRepository<MainWarehouse>().AddAsync(mainWarehouse);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Main warehouse added successfully!" });
            }

            return Json(new { success = false, message = "Invalid data. Please check your inputs." });
        }


        public async Task<IActionResult> WarehouseReport()
        {
            var warehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            ViewBag.Show = null;
            ViewBag.Warehouses = new SelectList(warehouses, "Id", "NameEn");
            ViewBag.Categories = new SelectList(categories, "Id", "NameEn");

            return View();
        }



    }
}