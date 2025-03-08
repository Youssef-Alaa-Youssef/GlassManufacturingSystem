using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Home;
using Factory.PL.Helper;
using Factory.PL.ViewModels.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Factory.PL.Controllers
{
    [Authorize(Roles = $"{nameof(UserRole.Owner)}, {nameof(UserRole.GM)}")]
    public class PropertyController : Controller
    {
        private readonly string _imagePath;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyController(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _imagePath = appSettings.Value.ImagePath;
            _unitOfWork = unitOfWork;
        }

        // GET: Property/Create
        public IActionResult Create()
        {
            return View(new PropertyViewModel());
        }

        // POST: Property/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyViewModel model, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (photos != null && photos.Length > 0)
                    {
                        foreach (var photo in photos)
                        {
                            if (photo.Length > 0)
                            {
                                // Validate image dimensions (425x388)
                                /* using (var image = Image.FromStream(photo.OpenReadStream()))
                                 {
                                     if (image.Width != 425 || image.Height != 388)
                                     {
                                         ModelState.AddModelError("Photos", "Each photo must be 425x388 pixels.");
                                         return View(model);
                                     }
                                 }*/

                                // Save the photo if it passes the validation
                                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
                                var filePath = Path.Combine("wwwroot", "images", "properties", fileName);

                                var directoryPath = Path.GetDirectoryName(filePath);
                                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await photo.CopyToAsync(stream);
                                }

                                model.PhotoUrls.Add($"/images/properties/{fileName}");
                            }
                        }
                    }

                    var property = new Property
                    {
                        Address = model.Address,
                        City = model.City,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        Price = model.Price,
                        SquareFootage = model.SquareFootage,
                        Bedrooms = model.Bedrooms,
                        Bathrooms = model.Bathrooms,
                        PropertyType = model.PropertyType,
                        ListedDate = model.ListedDate,
                        Description = model.Description,
                        Features = model.Features,
                        HasGarage = model.HasGarage,
                        HasPool = model.HasPool,
                        IsFurnished = model.IsFurnished,
                        AgentName = model.AgentName,
                        AgentEmail = model.AgentEmail,
                        AgentPhone = model.AgentPhone,
                        IsDeleted = model.IsDeleted,
                        CreatedDate = model.CreatedDate,
                        IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                        PhotoUrls = model.PhotoUrls
                    };

                    await _unitOfWork.GetRepository<Property>().AddAsync(property);
                    await _unitOfWork.SaveChangesAsync();

                    TempData["Success"] = "Property created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the property.";
                    Console.WriteLine(ex.Message);
                }
            }

            return View(model);
        }

        // GET: Property/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var model = new PropertyViewModel
            {
                Id = property.Id,
                Address = property.Address,
                City = property.City,
                State = property.State,
                ZipCode = property.ZipCode,
                Price = property.Price,
                SquareFootage = property.SquareFootage,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                PropertyType = property.PropertyType,
                ListedDate = property.ListedDate,
                Description = property.Description,
                Features = property.Features,
                PhotoUrls = property.PhotoUrls
            };

            return View(model);
        }

        // POST: Property/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Guid id, PropertyViewModel model, List<IFormFile> photos)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            // Update images
            if (photos != null && photos.Count > 0)
            {
                foreach (var photo in photos)
                {
                    if (photo.Length > 0)
                    {
                        var filePath = Path.Combine(_imagePath, Path.GetFileName(photo.FileName));
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }
                        property.PhotoUrls.Add($"/images/properties/{Path.GetFileName(photo.FileName)}");
                    }
                }
            }

            property.Address = model.Address;
            property.City = model.City;
            property.State = model.State;
            property.ZipCode = model.ZipCode;
            property.Price = model.Price;
            property.SquareFootage = model.SquareFootage;
            property.Bedrooms = model.Bedrooms;
            property.Bathrooms = model.Bathrooms;
            property.PropertyType = model.PropertyType;
            property.ListedDate = model.ListedDate;
            property.Description = model.Description;
            property.Features = model.Features;
            property.Status = model.Status;
            property.HasGarage = model.HasGarage;
            property.HasPool = model.HasPool;
            property.IsFurnished = model.IsFurnished;
            property.AgentName = model.AgentName;
            property.AgentEmail = model.AgentEmail;
            property.AgentPhone = model.AgentPhone;
            property.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.GetRepository<Property>().UpdateAsync(property);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Property/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            await _unitOfWork.GetRepository<Property>().RemoveAsync(property);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var properties = await _unitOfWork.GetRepository<Property>().GetAllAsync();

                var propertyViewModels = properties.Select(p => new PropertyViewModel
                {
                    Id = p.Id,
                    Address = p.Address,
                    City = p.City,
                    State = p.State,
                    ZipCode = p.ZipCode,
                    Price = p.Price,
                    SquareFootage = p.SquareFootage,
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    ListedDate = p.ListedDate,
                    Description = p.Description,
                    Features = p.Features,
                    PhotoUrls = p.PhotoUrls
                }).ToList();

                // Pass the property count to the view
                ViewBag.PropertyCount = propertyViewModels.Count;

                return View(propertyViewModels);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                ViewBag.PropertyCount = 0;
                return View(new List<PropertyViewModel>());
            }
        }
    }
}
