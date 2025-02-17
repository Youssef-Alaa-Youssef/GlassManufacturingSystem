using Microsoft.AspNetCore.Mvc;
using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Factory.DAL.Models.Warehouses;
using Microsoft.AspNetCore.Authorization;
using Factory.PL.ViewModels.Warehouses;
using System.Linq;
using System.Threading.Tasks;
using System;
using Factory.BLL.InterFaces;

namespace Factory.Controllers.Warehouses
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Item/Index
        public async Task<IActionResult> Index()
        {
            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync();
            var itemViewModels = items.Select(i => new ItemDetailsViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Type = i.Type,
                Color = i.Color,
                Thickness = i.Thickness,
                Dimensions = $"{i.Width} x {i.Height}",  // Show dimensions as a formatted string
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                WarehouseName = i.Warehouse?.NameEn,
                SubWarehouseName = i.SubWarehouse?.NameEn,
                Manufacturer = i.Manufacturer,
                ManufactureDate = i.ManufactureDate,
                ExpiryDate = i.ExpiryDate,
                IsFragile = i.IsFragile,
                Notes = i.Notes
            }).ToList();

            return View(itemViewModels);
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new ItemDetailsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Color = item.Color,
                Thickness = item.Thickness,
                Dimensions = $"{item.Width} x {item.Height}",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                WarehouseName = item.Warehouse?.NameEn,
                SubWarehouseName = item.SubWarehouse?.NameEn,
                Manufacturer = item.Manufacturer,
                ManufactureDate = item.ManufactureDate,
                ExpiryDate = item.ExpiryDate,
                IsFragile = item.IsFragile,
                Notes = item.Notes
            };

            return View(viewModel);
        }

        // GET: Item/Create
        public async Task<IActionResult> Create()
        {
            var warehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

            var viewModel = new ItemViewModel
            {
                Warehouses = warehouses.Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.NameEn
                }),
                SubWarehouses = subWarehouses.Select(sw => new SelectListItem
                {
                    Value = sw.Id.ToString(),
                    Text = sw.NameEn
                }),
                ManufactureDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(2)
            };

            return View(viewModel);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Type = viewModel.Type,
                    Color = viewModel.Color,
                    Thickness = viewModel.Thickness,
                    Width = viewModel.Width,
                    Height = viewModel.Height,
                    Quantity = viewModel.Quantity,
                    UnitPrice = viewModel.UnitPrice,
                    WarehouseId = viewModel.WarehouseId,
                    SubWarehouseId = viewModel.SubWarehouseId,
                    Manufacturer = viewModel.Manufacturer,
                    ManufactureDate = viewModel.ManufactureDate,
                    ExpiryDate = viewModel.ExpiryDate,
                    IsFragile = viewModel.IsFragile,
                    Notes = viewModel.Notes
                };

                try
                {
                    await _unitOfWork.GetRepository<Item>().AddAsync(item);
                    TempData["Success"] = "Item created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the item. Exception: {ex.Message}";
                }
            }

            await PopulateDropDownLists(viewModel);
            return View(viewModel);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Color = item.Color,
                Thickness = item.Thickness,
                Width = item.Width,
                Height = item.Height,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                WarehouseId = item.WarehouseId,
                SubWarehouseId = item.SubWarehouseId,
                Manufacturer = item.Manufacturer,
                ManufactureDate = item.ManufactureDate,
                ExpiryDate = item.ExpiryDate,
                IsFragile = item.IsFragile,
                Notes = item.Notes
            };

            await PopulateDropDownLists(viewModel);
            return View(viewModel);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                item.Name = viewModel.Name;
                item.Description = viewModel.Description;
                item.Type = viewModel.Type;
                item.Color = viewModel.Color;
                item.Thickness = viewModel.Thickness;
                item.Width = viewModel.Width;
                item.Height = viewModel.Height;
                item.Quantity = viewModel.Quantity;
                item.UnitPrice = viewModel.UnitPrice;
                item.WarehouseId = viewModel.WarehouseId;
                item.SubWarehouseId = viewModel.SubWarehouseId;
                item.Manufacturer = viewModel.Manufacturer;
                item.ManufactureDate = viewModel.ManufactureDate;
                item.ExpiryDate = viewModel.ExpiryDate;
                item.IsFragile = viewModel.IsFragile;
                item.Notes = viewModel.Notes;

                try
                {
                    await _unitOfWork.GetRepository<Item>().UpdateAsync(item);
                    TempData["Success"] = "Item updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the item. Exception: {ex.Message}";
                }
            }

            await PopulateDropDownLists(viewModel);
            return View(viewModel);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new ItemDetailsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Color = item.Color,
                Thickness = item.Thickness,
                Dimensions = $"{item.Width} x {item.Height}",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                WarehouseName = item.Warehouse?.NameEn,
                SubWarehouseName = item.SubWarehouse?.NameEn,
                Manufacturer = item.Manufacturer,
                ManufactureDate = item.ManufactureDate,
                ExpiryDate = item.ExpiryDate,
                IsFragile = item.IsFragile,
                Notes = item.Notes
            };

            return View(viewModel);
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<Item>().RemoveAsync(item);
                    TempData["Success"] = "Item deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the item. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Item not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDownLists(ItemViewModel viewModel)
        {
            var warehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

            viewModel.Warehouses = warehouses.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.NameEn
            });

            viewModel.SubWarehouses = subWarehouses.Select(sw => new SelectListItem
            {
                Value = sw.Id.ToString(),
                Text = sw.NameEn
            });
        }
    }
}
