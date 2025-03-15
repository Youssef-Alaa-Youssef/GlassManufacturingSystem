using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Models;
using Factory.DAL.Models.Warehouses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Factory.Controllers
{
    [Route("Items")]
    public class ItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IUnitOfWork unitOfWork, ILogger<ItemsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet("List")]

        public IActionResult Index(int categoryId)
        {
            var items = _unitOfWork.GetRepository<Item>().Query()
                .Where(i => i.CategoryId == categoryId)
                .ToList();

            ViewBag.CategoryId = categoryId;
            return View(items);
        }

        [HttpGet("AddItem")]
        public async Task<IActionResult> AddItem()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "NameEn"); 
            ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
            ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly

            return View();
        }


        [HttpPost("AddItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem([FromForm] Item item)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
                var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
                var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

                ViewBag.Categories = new SelectList(categories, "Id", "NameEn");
                ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
                ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly


                return View(item);
            }

            try
            {
                await _unitOfWork.GetRepository<Item>().AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", new { categoryId = item.CategoryId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item.");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the item.");

                var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
                var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
                var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

                ViewBag.Categories = new SelectList(categories, "Id", "NameEn");
                ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
                ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly

                return View(item);
            }
        }

        [HttpGet("EditItem/{id}")]
        public async Task<IActionResult> EditItem(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
            var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "NameEn");
            ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
            ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly

            return View(item);
        }

        [HttpPost("EditItem/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id, [FromForm] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
                var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
                var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

                ViewBag.Categories = new SelectList(categories, "Id", "NameEn");
                ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
                ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly


                return View(item);
            }

            try
            {
                await _unitOfWork.GetRepository<Item>().UpdateAsync(item);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", new { categoryId = item.CategoryId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item.");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the item.");

                var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
                var mainWarehouses = await _unitOfWork.GetRepository<MainWarehouse>().GetAllAsync();
                var subWarehouses = await _unitOfWork.GetRepository<SubWarehouse>().GetAllAsync();

                ViewBag.Categories = new SelectList(categories, "Id", "NameEn");
                ViewBag.MainWarehouses = new SelectList(mainWarehouses, "Id", "NameEn"); // Adjust properties accordingly
                ViewBag.SubWarehouses = new SelectList(subWarehouses, "Id", "NameEn"); // Adjust properties accordingly


                return View(item);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }


        [HttpPost("DeleteItem/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _unitOfWork.GetRepository<Item>().GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                await _unitOfWork.GetRepository<Item>().RemoveAsync(item);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Item deleted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item.");
                return Json(new { success = false, message = "An error occurred while deleting the item." });
            }
        }


        [HttpGet("GetItems")]
        public IActionResult GetItems(int Id)
        {
            try
            {
                var items = _unitOfWork.GetRepository<Item>().Query()
                    .Where(i => i.CategoryId == Id)
                    .Select(i => new
                    {
                        i.Id,
                        i.CodeNumber,
                        i.NameEn,
                        i.NameAr,
                        i.UnitPrice,
                        i.CurrentStock,
                        IsLowStock = i.IsLowStock()
                    })
                    .ToList();

                var totalItems = items.Count;
                var lowStockItems = items.Count(i => i.IsLowStock);

                var result = new
                {
                    Items = items,
                    Insights = new
                    {
                        TotalItems = totalItems,
                        LowStockItems = lowStockItems
                    }
                };
                ViewBag.Show = Id;

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while fetching items." });
            }
        }
        [HttpGet("Countries")]

        public async Task<IActionResult> Countries()
        {
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync();
            return View(countries);
        }
    }
}