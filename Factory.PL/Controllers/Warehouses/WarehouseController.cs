using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Warehouses;
using Factory.PL.ViewModels.Warehouses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                var mainWarehouse = new MainWarehouse
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    AddressEn = model.AddressEn,
                    AddressAr = model.AddressAr,
                    Capacity = model.Capacity,
                    CurrentStock = model.CurrentStock,
                    Type = model.Type,
                    Status = model.Status,
                    Manager = model.Manager,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                };

                await _unitOfWork.GetRepository<MainWarehouse>().AddAsync(mainWarehouse);
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

            var model = new MainWarehouse
            {
                Id = mainWarehouse.Id,
                NameEn = mainWarehouse.NameEn,
                NameAr = mainWarehouse.NameAr,
                AddressEn = mainWarehouse.AddressEn,
                AddressAr = mainWarehouse.AddressAr,
                Capacity = mainWarehouse.Capacity,
                CurrentStock = mainWarehouse.CurrentStock,
                Type = mainWarehouse.Type,
                Status = mainWarehouse.Status,
                Manager = mainWarehouse.Manager,
                PhoneNumber = mainWarehouse.PhoneNumber,
                Email = mainWarehouse.Email
            };

            return View(model);
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
                var mainWarehouse = await _unitOfWork.GetRepository<MainWarehouse>().GetByIdAsync(id);
                if (mainWarehouse == null)
                {
                    return NotFound();
                }

                mainWarehouse.NameEn = model.NameEn;
                mainWarehouse.NameAr = model.NameAr;
                mainWarehouse.AddressEn = model.AddressEn;
                mainWarehouse.AddressAr = model.AddressAr;
                mainWarehouse.Capacity = model.Capacity;
                mainWarehouse.CurrentStock = model.CurrentStock;
                mainWarehouse.Type = model.Type;
                mainWarehouse.Status = model.Status;
                mainWarehouse.Manager = model.Manager;
                mainWarehouse.PhoneNumber = model.PhoneNumber;
                mainWarehouse.Email = model.Email;

                await _unitOfWork.GetRepository<MainWarehouse>().UpdateAsync(mainWarehouse);
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
    }
}