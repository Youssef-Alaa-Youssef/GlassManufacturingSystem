using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Warehouses;
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

        // GET: Warehouse/Index
        public async Task<IActionResult> Index()
        {
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync(includeProperties: "SubWarehouses");
            return View(mainWarehouses);
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id, includeProperties: "SubWarehouses");
            if (mainWarehouse == null)
            {
                return NotFound();
            }
            return View(mainWarehouse);
        }

        // GET: Warehouse/Create
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

        public async Task<IActionResult> Delete(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
            if (mainWarehouse == null)
            {
                return NotFound();
            }
            return View(mainWarehouse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
            if (mainWarehouse == null)
            {
                return NotFound();
            }

            await _unitOfWork.GetRepository<MainWarehouse>().RemoveAsync(mainWarehouse);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Main warehouse deleted successfully!";
            return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(ManageSubWarehouses), new { id = model.MainWarehouseId });
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
                return RedirectToAction(nameof(ManageSubWarehouses), new { id = model.MainWarehouseId });
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

        public async Task<IActionResult> DeleteSubWarehouse(int id)
        {
            var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
            if (subWarehouse == null)
            {
                return NotFound();
            }
            return View(subWarehouse);
        }

        [HttpPost, ActionName("DeleteSubWarehouse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubWarehouseConfirmed(int id)
        {
            var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
            if (subWarehouse == null)
            {
                return NotFound();
            }

            await _unitOfWork.GetRepository<SubWarehouse>().RemoveAsync(subWarehouse);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Sub-warehouse deleted successfully!";
            return RedirectToAction(nameof(ManageSubWarehouses), new { id = subWarehouse.MainWarehouseId });
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

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                    GlassType = model.GlassType,
                    IsActive = true
                };

                await _unitOfWork.GetRepository<Category>().AddAsync(category);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Category added successfully!" });
            }

            return Json(new { success = false, message = "Invalid data. Please check your inputs." });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new Item
                {
                    CodeNumber = model.CodeNumber,
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    UnitPrice = model.UnitPrice,
                    MinimumStock = model.MinimumStock,
                    CurrentStock = model.CurrentStock,
                    Thickness = model.Thickness,
                    Width = model.Width,
                    Height = model.Height,
                    Color = model.Color,
                    Quality = model.Quality,
                    IsToughened = model.IsToughened,
                    IsLaminated = model.IsLaminated,
                    CategoryId = model.CategoryId,
                    MainWarehouseId = model.MainWarehouseId
                };

                await _unitOfWork.GetRepository<Item>().AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Item added successfully!" });
            }

            return Json(new { success = false, message = "Invalid data. Please check your inputs." });
        }
        public async Task<IActionResult> GetCategories([FromQuery]int warehouseId)
        {
            var categories = await _unitOfWork.GetRepository<Category>().Query()
                .Where(c => c.MainWarehouseId == warehouseId)
                .ToListAsync();
            var categoryList = categories.Select(c => new { Id = c.Id, NameEn = c.NameEn }).ToList();
            return Json(categoryList);
        }
        public async Task<IActionResult> GetItems([FromQuery] int categoryId)
        {
            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync(i => i.CategoryId == categoryId);
            var itemList = items.Select(i => new { Id = i.Id, NameEn = i.NameEn }).ToList();
            return Json(itemList);
        }
        public async Task<IActionResult> GetItemDetails([FromQuery] int itemId)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var itemDetails = new
            {
                item.CodeNumber,
                item.NameEn,
                item.NameAr,
                item.UnitPrice,
                item.CurrentStock,
                item.MinimumStock,
                item.Thickness,
                item.Width,
                item.Height,
                item.Color,
                item.Quality,
                item.IsToughened,
                item.IsLaminated
            };

            return Json(itemDetails);
        }
    }
}