using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Factory.BLL.InterFaces;
using Factory.DAL.Models.Warehouses;
using Factory.PL.ViewModels.Warehouses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.PL.Controllers.Warehouses
{
        [Authorize(Roles = "SuperAdmin")]
        public class WarehouseController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;

            public WarehouseController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IActionResult> Index()
            {
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
            var model = new MainWarehouseViewModel();
            return View(model);
        }


        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(MainWarehouseViewModel model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var mainWarehouse = new MainWarehouse
                        {
                            NameEn = model.NameEn,
                            NameAr = model.NameAr,
                            AddressEn = model.AddressEn,
                            AddressAr = model.AddressAr
                        };

                        await _unitOfWork.GetRepository<MainWarehouse>().AddAsync(mainWarehouse);
                        TempData["Success"] = "Main warehouse created successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = $"An error occurred: {ex.Message}";
                    }
                }

                return View(model);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
                if (mainWarehouse == null)
                {
                    return NotFound();
                }

                var model = new MainWarehouseViewModel
                {
                    Id = mainWarehouse.Id,
                    NameEn = mainWarehouse.NameEn,
                    NameAr = mainWarehouse.NameAr,
                    AddressEn = mainWarehouse.AddressEn,
                    AddressAr = mainWarehouse.AddressAr
                };

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, MainWarehouseViewModel model)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
                        if (mainWarehouse == null)
                        {
                            return NotFound();
                        }

                        mainWarehouse.NameEn = model.NameEn;
                        mainWarehouse.NameAr = model.NameAr;
                        mainWarehouse.AddressEn = model.AddressEn;
                        mainWarehouse.AddressAr = model.AddressAr;

                        await _unitOfWork.GetRepository<MainWarehouse>().UpdateAsync(mainWarehouse);
                        TempData["Success"] = "Main warehouse updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = $"An error occurred: {ex.Message}";
                    }
                }

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
                try
                {
                    var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
                    if (mainWarehouse != null)
                    {
                        await _unitOfWork.GetRepository<MainWarehouse>().RemoveAsync(mainWarehouse);
                        TempData["Success"] = "Main warehouse deleted successfully!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred: {ex.Message}";
                }

                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> ManageSubWarehouses(int mainWarehouseId)
            {
                var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(mainWarehouseId, includeProperties: "SubWarehouses");
                if (mainWarehouse == null)
                {
                    return NotFound();
                }

                return View(mainWarehouse);
            }

        public async Task<IActionResult> CreateSubWarehouse()
        {
            var mainWarehouses =await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();

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
                try
                {
                    // Ensure MainWarehouse exists
                    var mainWarehouseExists = await _unitOfWork.GetRepository<MainWarehouse>()
                        .FindAsync(mw => mw.Id == model.MainWarehouseId);


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
                    return RedirectToAction(nameof(ManageSubWarehouses), new { mainWarehouseId = model.MainWarehouseId });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred: {ex.Message} | Inner Exception: {ex.InnerException?.Message}";
                }
            }

            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            model.MainWarehouses = mainWarehouses.Select(mw => new SelectListItem
            {
                Value = mw.Id.ToString(),
                Text = mw.NameEn
            });

            return View(model);
        }

        public async Task<IActionResult> EditSubWarehouse(int id)
            {
                var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
                if (subWarehouse == null)
                {
                    return NotFound();
                }

                var model = new SubWarehouseViewModel
                {
                    Id = subWarehouse.Id,
                    NameEn = subWarehouse.NameEn,
                    NameAr = subWarehouse.NameAr,
                    AddressEn = subWarehouse.AddressEn,
                    AddressAr = subWarehouse.AddressAr,
                    MainWarehouseId = subWarehouse.MainWarehouseId
                };

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditSubWarehouse(int id, SubWarehouseViewModel model)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
                        if (subWarehouse == null)
                        {
                            return NotFound();
                        }

                        subWarehouse.NameEn = model.NameEn;
                        subWarehouse.NameAr = model.NameAr;
                        subWarehouse.AddressEn = model.AddressEn;
                        subWarehouse.AddressAr = model.AddressAr;

                        await _unitOfWork.GetRepository<SubWarehouse>().UpdateAsync(subWarehouse);
                        TempData["Success"] = "Sub-warehouse updated successfully!";
                        return RedirectToAction(nameof(ManageSubWarehouses), new { mainWarehouseId = model.MainWarehouseId });
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = $"An error occurred: {ex.Message}";
                    }
                }

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
                try
                {
                    var subWarehouse = await _unitOfWork.GetRepository<SubWarehouse>().GetByIdAsync(id);
                    if (subWarehouse != null)
                    {
                        await _unitOfWork.GetRepository<SubWarehouse>().RemoveAsync(subWarehouse);
                        TempData["Success"] = "Sub-warehouse deleted successfully!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred: {ex.Message}";
                }

                return RedirectToAction(nameof(Index));
            }
        }
}