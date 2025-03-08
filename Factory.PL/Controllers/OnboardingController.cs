using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.OnBoarding;
using Factory.PL.ViewModels.OnBoarding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnboardingSystem.Controllers
{
    public class OnboardingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OnboardingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var onboardingProcesses = await _unitOfWork.GetRepository<OnboardingProcess>()
                .Query()
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();

            return View(onboardingProcesses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onboardingProcess = await _unitOfWork.GetRepository<OnboardingProcess>()
                .Query()
                .Include(o => o.Preboarding)
                .Include(o => o.ITSetup)
                .Include(o => o.Training)
                .Include(o => o.Orientation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (onboardingProcess == null)
            {
                return NotFound();
            }

            var viewModel = new OnboardingDetailsViewModel
            {
                OnboardingProcess = onboardingProcess
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OnboardingCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var onboardingProcess = new OnboardingProcess
                {
                    EmployeeName = viewModel.EmployeeName,
                    EmployeeId = viewModel.EmployeeId,
                    StartDate = viewModel.StartDate,
                    Status = OnboardingStatus.NotStarted,
                    Preboarding = new PreboardingModule(),
                    ITSetup = new ITSetupModule(),
                    Training = new TrainingModule(),
                    Orientation = new OrientationModule()
                };

                await _unitOfWork.GetRepository<OnboardingProcess>().AddAsync(onboardingProcess);
                return RedirectToAction(nameof(Details), new { id = onboardingProcess.Id });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onboardingProcess = await _unitOfWork.GetRepository<OnboardingProcess>()
                .Query()
                .Include(o => o.Preboarding)
                .Include(o => o.ITSetup)
                .Include(o => o.Training)
                .Include(o => o.Orientation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (onboardingProcess == null)
            {
                return NotFound();
            }

            var viewModel = new OnboardingEditViewModel
            {
                Id = onboardingProcess.Id,
                EmployeeName = onboardingProcess.EmployeeName,
                EmployeeId = onboardingProcess.EmployeeId,
                StartDate = onboardingProcess.StartDate,
                Status = onboardingProcess.Status,
                Preboarding = onboardingProcess.Preboarding,
                ITSetup = onboardingProcess.ITSetup,
                Training = onboardingProcess.Training,
                Orientation = onboardingProcess.Orientation
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OnboardingEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var onboardingProcess = await _unitOfWork.GetRepository<OnboardingProcess>()
                        .Query()
                        .Include(o => o.Preboarding)
                        .Include(o => o.ITSetup)
                        .Include(o => o.Training)
                        .Include(o => o.Orientation)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (onboardingProcess == null)
                    {
                        return NotFound();
                    }

                    onboardingProcess.EmployeeName = viewModel.EmployeeName;
                    onboardingProcess.EmployeeId = viewModel.EmployeeId;
                    onboardingProcess.StartDate = viewModel.StartDate;
                    onboardingProcess.Status = viewModel.Status;

                    UpdatePreboardingModule(onboardingProcess.Preboarding, viewModel.Preboarding);
                    UpdateITSetupModule(onboardingProcess.ITSetup, viewModel.ITSetup);
                    UpdateTrainingModule(onboardingProcess.Training, viewModel.Training);
                    UpdateOrientationModule(onboardingProcess.Orientation, viewModel.Orientation);

                    CheckAndUpdateCompletionStatus(onboardingProcess);

                    await _unitOfWork.GetRepository<OnboardingProcess>().UpdateAsync(onboardingProcess);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OnboardingProcessExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Preboarding(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preboardingModule = await _unitOfWork.GetRepository<PreboardingModule>()
                .Query()
                .Include(p => p.OnboardingProcess)
                .FirstOrDefaultAsync(m => m.OnboardingProcessId == id);

            if (preboardingModule == null)
            {
                return NotFound();
            }

            var viewModel = new PreboardingViewModel
            {
                OnboardingProcessId = preboardingModule.OnboardingProcessId,
                EmployeeName = preboardingModule.OnboardingProcess.EmployeeName,
                Module = preboardingModule
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePreboarding(PreboardingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var module = await _unitOfWork.GetRepository<PreboardingModule>()
                    .Query()
                    .FirstOrDefaultAsync(m => m.Id == viewModel.Module.Id);

                if (module == null)
                {
                    return NotFound();
                }

                UpdatePreboardingModule(module, viewModel.Module);

                if (module.DocumentsReceived && module.ContractSigned &&
                    module.BackgroundCheckCompleted && module.WelcomeEmailSent)
                {
                    module.CompletionDate = DateTime.Now;
                }

                await _unitOfWork.GetRepository<PreboardingModule>().UpdateAsync(module);

                await UpdateOnboardingStatus(viewModel.OnboardingProcessId);

                return RedirectToAction(nameof(Details), new { id = viewModel.OnboardingProcessId });
            }

            return View("Preboarding", viewModel);
        }

        public async Task<IActionResult> ITSetup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itSetupModule = await _unitOfWork.GetRepository<ITSetupModule>()
                .Query()
                .Include(i => i.OnboardingProcess)
                .FirstOrDefaultAsync(m => m.OnboardingProcessId == id);

            if (itSetupModule == null)
            {
                return NotFound();
            }

            var viewModel = new ITSetupViewModel
            {
                OnboardingProcessId = itSetupModule.OnboardingProcessId,
                EmployeeName = itSetupModule.OnboardingProcess.EmployeeName,
                Module = itSetupModule
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateITSetup(ITSetupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var module = await _unitOfWork.GetRepository<ITSetupModule>()
                    .Query()
                    .FirstOrDefaultAsync(m => m.Id == viewModel.Module.Id);

                if (module == null)
                {
                    return NotFound();
                }

                UpdateITSetupModule(module, viewModel.Module);

                if (module.EmailSetup && module.HardwareProvisioned &&
                    module.SoftwareInstalled && module.AccessGranted)
                {
                    module.CompletionDate = DateTime.Now;
                }

                await _unitOfWork.GetRepository<ITSetupModule>().UpdateAsync(module);

                await UpdateOnboardingStatus(viewModel.OnboardingProcessId);

                return RedirectToAction(nameof(Details), new { id = viewModel.OnboardingProcessId });
            }

            return View("ITSetup", viewModel);
        }


        public async Task<IActionResult> Training(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingModule = await _unitOfWork.GetRepository<TrainingModule>()
                .Query()
                .Include(t => t.OnboardingProcess)
                .FirstOrDefaultAsync(m => m.OnboardingProcessId == id);

            if (trainingModule == null)
            {
                return NotFound();
            }

            var viewModel = new TrainingViewModel
            {
                OnboardingProcessId = trainingModule.OnboardingProcessId,
                EmployeeName = trainingModule.OnboardingProcess.EmployeeName,
                Module = trainingModule
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTraining(TrainingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var module = await _unitOfWork.GetRepository<TrainingModule>()
                    .Query()
                    .FirstOrDefaultAsync(m => m.Id == viewModel.Module.Id);

                if (module == null)
                {
                    return NotFound();
                }

                UpdateTrainingModule(module, viewModel.Module);

                if (module.ComplianceTraining && module.SkillsTraining &&
                    module.SystemsTraining && module.SecurityTraining)
                {
                    module.CompletionDate = DateTime.Now;
                }

                await _unitOfWork.GetRepository<TrainingModule>().UpdateAsync(module);

                await UpdateOnboardingStatus(viewModel.OnboardingProcessId);

                return RedirectToAction(nameof(Details), new { id = viewModel.OnboardingProcessId });
            }

            return View("Training", viewModel);
        }



        // Helper methods
        private async Task<bool> OnboardingProcessExists(int id)
        {
            return await _unitOfWork.GetRepository<OnboardingProcess>()
                .Query()
                .AnyAsync(e => e.Id == id);
        }

        private void UpdatePreboardingModule(PreboardingModule target, PreboardingModule source)
        {
            target.DocumentsReceived = source.DocumentsReceived;
            target.ContractSigned = source.ContractSigned;
            target.BackgroundCheckCompleted = source.BackgroundCheckCompleted;
            target.WelcomeEmailSent = source.WelcomeEmailSent;
            target.Notes = source.Notes;

            if (target.DocumentsReceived && target.ContractSigned &&
                target.BackgroundCheckCompleted && target.WelcomeEmailSent)
            {
                target.CompletionDate = DateTime.Now;
            }
        }

        private void UpdateITSetupModule(ITSetupModule target, ITSetupModule source)
        {
            target.EmailSetup = source.EmailSetup;
            target.HardwareProvisioned = source.HardwareProvisioned;
            target.SoftwareInstalled = source.SoftwareInstalled;
            target.AccessGranted = source.AccessGranted;
            target.Notes = source.Notes;

            if (target.EmailSetup && target.HardwareProvisioned &&
                target.SoftwareInstalled && target.AccessGranted)
            {
                target.CompletionDate = DateTime.Now;
            }
        }

        private void UpdateTrainingModule(TrainingModule target, TrainingModule source)
        {
            target.ComplianceTraining = source.ComplianceTraining;
            target.SkillsTraining = source.SkillsTraining;
            target.SystemsTraining = source.SystemsTraining;
            target.SecurityTraining = source.SecurityTraining;
            target.Notes = source.Notes;

            if (target.ComplianceTraining && target.SkillsTraining &&
                target.SystemsTraining && target.SecurityTraining)
            {
                target.CompletionDate = DateTime.Now;
            }
        }

        private void UpdateOrientationModule(OrientationModule target, OrientationModule source)
        {
            target.CompanyOrientationCompleted = source.CompanyOrientationCompleted;
            target.DepartmentOrientationCompleted = source.DepartmentOrientationCompleted;
            target.MentorAssigned = source.MentorAssigned;
            target.FirstWeekCheckInCompleted = source.FirstWeekCheckInCompleted;
            target.Notes = source.Notes;

            if (target.CompanyOrientationCompleted && target.DepartmentOrientationCompleted &&
                target.MentorAssigned && target.FirstWeekCheckInCompleted)
            {
                target.CompletionDate = DateTime.Now;
            }
        }

        private void CheckAndUpdateCompletionStatus(OnboardingProcess process)
        {
            bool allCompleted =
                process.Preboarding.CompletionDate.HasValue &&
                process.ITSetup.CompletionDate.HasValue &&
                process.Training.CompletionDate.HasValue &&
                process.Orientation.CompletionDate.HasValue;

            if (allCompleted)
            {
                process.Status = OnboardingStatus.Completed;
                process.CompletionDate = DateTime.Now;
            }
            else if (process.Status == OnboardingStatus.NotStarted)
            {
                process.Status = OnboardingStatus.InProgress;
            }
        }

        private async Task UpdateOnboardingStatus(int processId)
        {
            var process = await _unitOfWork.GetRepository<OnboardingProcess>()
                .Query()
                .Include(o => o.Preboarding)
                .Include(o => o.ITSetup)
                .Include(o => o.Training)
                .Include(o => o.Orientation)
                .FirstOrDefaultAsync(m => m.Id == processId);

            if (process != null)
            {
                CheckAndUpdateCompletionStatus(process);
                await _unitOfWork.GetRepository<OnboardingProcess>().UpdateAsync(process);
            }
        }
    }
}