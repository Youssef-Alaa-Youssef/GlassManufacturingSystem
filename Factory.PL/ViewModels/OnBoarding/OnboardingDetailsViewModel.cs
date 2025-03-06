using Factory.DAL.Models.OnBoarding;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class OnboardingDetailsViewModel
    {
        public OnboardingProcess OnboardingProcess { get; set; } = new OnboardingProcess();

        public int PreboardingProgressPercentage => CalculateModuleProgress(OnboardingProcess.Preboarding);
        public int ITSetupProgressPercentage => CalculateModuleProgress(OnboardingProcess.ITSetup);
        public int TrainingProgressPercentage => CalculateModuleProgress(OnboardingProcess.Training);
        public int OrientationProgressPercentage => CalculateModuleProgress(OnboardingProcess.Orientation);
        public int OverallProgressPercentage => (PreboardingProgressPercentage + ITSetupProgressPercentage + TrainingProgressPercentage + OrientationProgressPercentage) / 4;

        private int CalculateModuleProgress(PreboardingModule module)
        {
            int completed = 0;
            int total = 4;

            if (module.DocumentsReceived) completed++;
            if (module.ContractSigned) completed++;
            if (module.BackgroundCheckCompleted) completed++;
            if (module.WelcomeEmailSent) completed++;

            return (int)Math.Round((double)completed / total * 100);
        }

        private int CalculateModuleProgress(ITSetupModule module)
        {
            int completed = 0;
            int total = 4;

            if (module.EmailSetup) completed++;
            if (module.HardwareProvisioned) completed++;
            if (module.SoftwareInstalled) completed++;
            if (module.AccessGranted) completed++;

            return (int)Math.Round((double)completed / total * 100);
        }

        private int CalculateModuleProgress(TrainingModule module)
        {
            int completed = 0;
            int total = 4;

            if (module.ComplianceTraining) completed++;
            if (module.SkillsTraining) completed++;
            if (module.SystemsTraining) completed++;
            if (module.SecurityTraining) completed++;

            return (int)Math.Round((double)completed / total * 100);
        }

        private int CalculateModuleProgress(OrientationModule module)
        {
            int completed = 0;
            int total = 4;

            if (module.CompanyOrientationCompleted) completed++;
            if (module.DepartmentOrientationCompleted) completed++;
            if (module.MentorAssigned) completed++;
            if (module.FirstWeekCheckInCompleted) completed++;

            return (int)Math.Round((double)completed / total * 100);
        }
    }
}
