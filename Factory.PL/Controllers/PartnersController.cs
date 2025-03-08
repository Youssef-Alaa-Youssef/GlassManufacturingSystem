using Factory.BLL.InterFaces;
using Factory.DAL.Models.Home;
using Factory.PL.Services.UploadFile;
using Factory.PL.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;

namespace Factory.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PartnersController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public PartnersController(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var partners = await _unitOfWork.GetRepository<Partner>().GetAllAsync();
            return View(partners);
        }

        public async Task<IActionResult> Details(int id)
        {
            var partner = await _unitOfWork.GetRepository<Partner>().GetByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _unitOfWork.GetRepository<Partner>().GetByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            var partnerViewModel = new PartnerViewModel
            {
                Id = partner.Id,
                Name = partner.Name,
                LogoUrl = partner.LogoUrl,
                Description = partner.Description
            };

            return View(partnerViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = string.Empty;

                    if (model.LogoFile != null && model.LogoFile.Length > 0)
                    {
                        using (var image = Image.FromStream(model.LogoFile.OpenReadStream()))
                        {
                            if (image.Width != 250 || image.Height != 190)
                            {
                                ModelState.AddModelError("LogoFile", "The logo must be 250x190 pixels.");
                                return View(model);
                            }
                        }

                        filePath = await _fileService.UploadFileAsync(model.LogoFile, "Partners");
                    }

                    var partner = new Partner
                    {
                        Name = model.Name,
                        Description = model.Description,
                        CreatedAt = DateTime.UtcNow,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
                        LogoUrl = filePath
                    };

                    await _unitOfWork.GetRepository<Partner>().AddAsync(partner);
                    TempData["Success"] = "Partner created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the partner: {ex.Message}";
                    Console.WriteLine(ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartnerViewModel partnerViewModel)
        {
            if (id != partnerViewModel.Id)
            {
                ModelState.AddModelError(string.Empty, "Invalid partner ID.");
                return View(partnerViewModel);
            }

            if (ModelState.IsValid)
            {
                var partner = await _unitOfWork.GetRepository<Partner>().GetByIdAsync(id);
                if (partner == null)
                {
                    ModelState.AddModelError(string.Empty, "Partner not found.");
                    return View(partnerViewModel);
                }

                try
                {
                    // Map updated values
                    partner.Name = partnerViewModel.Name;
                    partner.Description = partnerViewModel.Description;
                    partner.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

                    if (partnerViewModel.LogoFile != null && partnerViewModel.LogoFile.Length > 0)
                    {
                        var directoryPath = Path.Combine("wwwroot", "images", "partners");
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        var fileName = Path.GetFileName(partnerViewModel.LogoFile.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await partnerViewModel.LogoFile.CopyToAsync(stream);
                        }

                        partner.LogoUrl = $"/images/partners/{fileName}";
                    }

                    await _unitOfWork.GetRepository<Partner>().UpdateAsync(partner);
                    ModelState.AddModelError(string.Empty, "Partner updated successfully!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the partner.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors below.");
            }

            return View(partnerViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _unitOfWork.GetRepository<Partner>().GetByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _unitOfWork.GetRepository<Partner>().GetByIdAsync(id);
            if (partner != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<Partner>().RemoveAsync(partner);
                    TempData["Success"] = "Partner deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the partner. {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Partner not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
