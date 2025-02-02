using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Factory.DAL.Models.Settings;
using Factory.PL.ViewModels.Settings;
using Factory.BLL.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Factory.BLL.InterFaces;

namespace Factory.Controllers.Warehouses
{
    [Authorize] // Ensure all actions require authentication
    public class SettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SettingController> _logger;

        public SettingController(IUnitOfWork unitOfWork, ILogger<SettingController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: Setting/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var settings = await _unitOfWork.GetRepository<Settings>().GetAllAsync();
                var settingViewModels = settings.Select(s => new SettingsViewModel
                {
                    Id = s.Id,
                    UserId = s.UserId, // Include UserId
                    FactoryNameAr = s.FactoryNameAr,
                    FactoryNameEn = s.FactoryNameEn,
                    FactoryLogo = s.FactoryLogo,
                    TaxNumber = s.TaxNumber,
                    Address = s.Address,
                    ContactNumber = s.ContactNumber,
                    Email = s.Email,
                    Website = s.Website,
                    Theme = s.Theme,
                    Language = s.Language,
                    ItemsPerPage = s.ItemsPerPage,
                    EnableNotifications = s.EnableNotifications,
                    NotificationPreferences = s.NotificationPreferences,
                    ShowDesign1 = s.ShowDesign1,
                    ShowDesign2 = s.ShowDesign2,
                    ShowDesign3 = s.ShowDesign3,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                }).ToList();

                return View(settingViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching settings.");
                TempData["Error"] = "An error occurred while fetching settings.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Setting/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var setting = await _unitOfWork.GetRepository<Settings>().GetByIdAsync(id);
                if (setting == null)
                {
                    return NotFound();
                }

                var viewModel = new SettingsViewModel
                {
                    Id = setting.Id,
                    UserId = setting.UserId, // Include UserId
                    FactoryNameAr = setting.FactoryNameAr,
                    FactoryNameEn = setting.FactoryNameEn,
                    FactoryLogo = setting.FactoryLogo,
                    TaxNumber = setting.TaxNumber,
                    Address = setting.Address,
                    ContactNumber = setting.ContactNumber,
                    Email = setting.Email,
                    Website = setting.Website,
                    Theme = setting.Theme,
                    Language = setting.Language,
                    ItemsPerPage = setting.ItemsPerPage,
                    EnableNotifications = setting.EnableNotifications,
                    NotificationPreferences = setting.NotificationPreferences,
                    ShowDesign1 = setting.ShowDesign1,
                    ShowDesign2 = setting.ShowDesign2,
                    ShowDesign3 = setting.ShowDesign3,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching setting details.");
                TempData["Error"] = "An error occurred while fetching setting details.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Setting/Create
        public IActionResult Create()
        {
            var viewModel = new SettingsViewModel
            {
                UserId = User.Identity.Name // Automatically set the UserId
            };
            return View(viewModel);
        }

        // POST: Setting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var setting = new Settings
                {
                    UserId = User.Identity.Name, // Set UserId from the logged-in user
                    FactoryNameAr = viewModel.FactoryNameAr,
                    FactoryNameEn = viewModel.FactoryNameEn,
                    FactoryLogo = viewModel.FactoryLogo,
                    TaxNumber = viewModel.TaxNumber,
                    Address = viewModel.Address,
                    ContactNumber = viewModel.ContactNumber,
                    Email = viewModel.Email,
                    Website = viewModel.Website,
                    Theme = viewModel.Theme,
                    Language = viewModel.Language,
                    ItemsPerPage = viewModel.ItemsPerPage,
                    EnableNotifications = viewModel.EnableNotifications,
                    NotificationPreferences = viewModel.NotificationPreferences,
                    ShowDesign1 = viewModel.ShowDesign1,
                    ShowDesign2 = viewModel.ShowDesign2,
                    ShowDesign3 = viewModel.ShowDesign3,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                try
                {
                    await _unitOfWork.GetRepository<Settings>().AddAsync(setting);
                    TempData["Success"] = "Setting created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the setting.");
                    TempData["Error"] = "An error occurred while creating the setting.";
                }
            }

            return View(viewModel);
        }

        // GET: Setting/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var setting = await _unitOfWork.GetRepository<Settings>().GetByIdAsync(id);
                if (setting == null)
                {
                    return NotFound();
                }

                var viewModel = new SettingsViewModel
                {
                    Id = setting.Id,
                    UserId = setting.UserId, // Include UserId
                    FactoryNameAr = setting.FactoryNameAr,
                    FactoryNameEn = setting.FactoryNameEn,
                    FactoryLogo = setting.FactoryLogo,
                    TaxNumber = setting.TaxNumber,
                    Address = setting.Address,
                    ContactNumber = setting.ContactNumber,
                    Email = setting.Email,
                    Website = setting.Website,
                    Theme = setting.Theme,
                    Language = setting.Language,
                    ItemsPerPage = setting.ItemsPerPage,
                    EnableNotifications = setting.EnableNotifications,
                    NotificationPreferences = setting.NotificationPreferences,
                    ShowDesign1 = setting.ShowDesign1,
                    ShowDesign2 = setting.ShowDesign2,
                    ShowDesign3 = setting.ShowDesign3,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the setting for editing.");
                TempData["Error"] = "An error occurred while fetching the setting for editing.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Setting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingsViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var setting = await _unitOfWork.GetRepository<Settings>().GetByIdAsync(id);
                    if (setting == null)
                    {
                        return NotFound();
                    }

                    setting.UserId = User.Identity.Name; // Update UserId
                    setting.FactoryNameAr = viewModel.FactoryNameAr;
                    setting.FactoryNameEn = viewModel.FactoryNameEn;
                    setting.FactoryLogo = viewModel.FactoryLogo;
                    setting.TaxNumber = viewModel.TaxNumber;
                    setting.Address = viewModel.Address;
                    setting.ContactNumber = viewModel.ContactNumber;
                    setting.Email = viewModel.Email;
                    setting.Website = viewModel.Website;
                    setting.Theme = viewModel.Theme;
                    setting.Language = viewModel.Language;
                    setting.ItemsPerPage = viewModel.ItemsPerPage;
                    setting.EnableNotifications = viewModel.EnableNotifications;
                    setting.NotificationPreferences = viewModel.NotificationPreferences;
                    setting.ShowDesign1 = viewModel.ShowDesign1;
                    setting.ShowDesign2 = viewModel.ShowDesign2;
                    setting.ShowDesign3 = viewModel.ShowDesign3;
                    setting.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.GetRepository<Settings>().UpdateAsync(setting);
                    TempData["Success"] = "Setting updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the setting.");
                    TempData["Error"] = "An error occurred while updating the setting.";
                }
            }

            return View(viewModel);
        }

        // GET: Setting/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var setting = await _unitOfWork.GetRepository<Settings>().GetByIdAsync(id);
                if (setting == null)
                {
                    return NotFound();
                }

                var viewModel = new SettingsViewModel
                {
                    Id = setting.Id,
                    UserId = setting.UserId, // Include UserId
                    FactoryNameAr = setting.FactoryNameAr,
                    FactoryNameEn = setting.FactoryNameEn,
                    FactoryLogo = setting.FactoryLogo,
                    TaxNumber = setting.TaxNumber,
                    Address = setting.Address,
                    ContactNumber = setting.ContactNumber,
                    Email = setting.Email,
                    Website = setting.Website,
                    Theme = setting.Theme,
                    Language = setting.Language,
                    ItemsPerPage = setting.ItemsPerPage,
                    EnableNotifications = setting.EnableNotifications,
                    NotificationPreferences = setting.NotificationPreferences,
                    ShowDesign1 = setting.ShowDesign1,
                    ShowDesign2 = setting.ShowDesign2,
                    ShowDesign3 = setting.ShowDesign3,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the setting for deletion.");
                TempData["Error"] = "An error occurred while fetching the setting for deletion.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Setting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var setting = await _unitOfWork.GetRepository<Settings>().GetByIdAsync(id);
                if (setting != null)
                {
                    await _unitOfWork.GetRepository<Settings>().RemoveAsync(setting);
                    TempData["Success"] = "Setting deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Setting not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the setting.");
                TempData["Error"] = "An error occurred while deleting the setting.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}