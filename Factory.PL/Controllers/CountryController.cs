using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factory.DAL.Models;
using Factory.PL.Services.UploadFile;
using Factory.PL.ViewModels;
using Factory.BLL.InterFaces;

namespace Factory.PL.Controllers
{
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileUploadService;

        public CountryController(IUnitOfWork unitOfWork, IFileService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync();
            var countryViewModels = countries.Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn,
                Code = c.Code,
                ImagePath = c.ImagePath
            }).ToList();

            return View(countryViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                if (countryViewModel.ImageFile != null && countryViewModel.ImageFile.Length > 0)
                {
                    try
                    {
                        countryViewModel.ImagePath = await _fileUploadService.UploadFileAsync(countryViewModel.ImageFile, "countries");
                    }
                    catch (InvalidOperationException ex)
                    {
                        ModelState.AddModelError("ImageFile", ex.Message);
                        return View(countryViewModel);
                    }
                }

                var country = new Country
                {
                    Name = countryViewModel.Name,
                    NameEn = countryViewModel.NameEn,
                    Code = countryViewModel.Code,
                    ImagePath = countryViewModel.ImagePath
                };

                await _unitOfWork.GetRepository<Country>().AddAsync(country);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _unitOfWork.GetRepository<Country>().GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            var countryViewModel = new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
                NameEn = country.NameEn,
                Code = country.Code,
                ImagePath = country.ImagePath
            };

            return View(countryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryViewModel countryViewModel)
        {
            if (id != countryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (countryViewModel.ImageFile != null && countryViewModel.ImageFile.Length > 0)
                    {
                        try
                        {
                            countryViewModel.ImagePath = await _fileUploadService.UploadFileAsync(countryViewModel.ImageFile, "countries");
                        }
                        catch (InvalidOperationException ex)
                        {
                            ModelState.AddModelError("ImageFile", ex.Message);
                            return View(countryViewModel);
                        }
                    }

                    var country = new Country
                    {
                        Id = countryViewModel.Id,
                        Name = countryViewModel.Name,
                        NameEn = countryViewModel.NameEn,
                        Code = countryViewModel.Code,
                        ImagePath = countryViewModel.ImagePath
                    };

                    await _unitOfWork.GetRepository<Country>().UpdateAsync(country);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CountryExists(countryViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(countryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _unitOfWork.GetRepository<Country>().GetByIdAsync(id);
            if (country != null)
            {
                await _unitOfWork.GetRepository<Country>().RemoveAsync(country);
                await _unitOfWork.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _unitOfWork.GetRepository<Country>().Query().AnyAsync(e => e.Id == id);
        }
    }
}