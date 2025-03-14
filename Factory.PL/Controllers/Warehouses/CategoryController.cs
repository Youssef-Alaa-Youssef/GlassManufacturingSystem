using Factory.DAL.Models.Warehouses;
using Microsoft.AspNetCore.Mvc;
using Factory.BLL.InterFaces;
using Factory.DAL.ViewModels.Warehouses;

namespace Factory.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int warehouseId, int subWarehouseId)
        {
            ViewBag.WarehouseId = warehouseId;
            ViewBag.SubWarehouseId = subWarehouseId;
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            return View(categories);
        }

        [HttpGet("AddCategory")]
        public IActionResult AddCategory(int warehouseId)
        {
            var viewModel = new CategoryViewModel
            {
                MainWarehouseId = warehouseId 
            };

            return View(viewModel);
        }

        [HttpPost("AddCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var category = ToDomainModel(viewModel);

            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index", new { warehouseId = viewModel.MainWarehouseId });
        }


        [HttpGet("EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost("EditCategory/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [FromForm] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { warehouseId = ViewBag.WarehouseId, subWarehouseId = ViewBag.SubWarehouseId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category.");
                return View(category);
            }
        }

        [HttpPost("DeleteCategory/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                await _unitOfWork.GetRepository<Category>().RemoveAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return Json(new { success = true, message = "Category deleted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category.");
                return Json(new { success = false, message = "An error occurred while deleting the category." });
            }
        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategories(int id)
        {
            try
            {
                var categories = _unitOfWork.GetRepository<Category>().Query()
                    .Where(c => c.MainWarehouseId == id)
                    .Select(c => new { c.Id, c.NameEn }) 
                    .ToList();

                return Json(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories for warehouse ID {WarehouseId}.", id);
                return Json(new { success = false, message = "An error occurred while fetching categories." });
            }
        }


        public static CategoryViewModel ToViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                NameEn = category.NameEn,
                NameAr = category.NameAr,
                DescriptionEn = category.DescriptionEn,
                DescriptionAr = category.DescriptionAr,
                MainWarehouseId = category.MainWarehouseId,
                MainWarehouseName = category.MainWarehouse?.NameEn
            };
        }

        public static Category ToDomainModel(CategoryViewModel viewModel)
        {
            return new Category
            {
                Id = viewModel.Id,
                NameEn = viewModel.NameEn,
                NameAr = viewModel.NameAr,
                DescriptionEn = viewModel.DescriptionEn,
                DescriptionAr = viewModel.DescriptionAr,
                MainWarehouseId = viewModel.MainWarehouseId
            };
        }

    }
}