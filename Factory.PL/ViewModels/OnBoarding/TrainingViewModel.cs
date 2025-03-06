using Factory.DAL.Models.OnBoarding;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class TrainingViewModel
    {
        public int OnboardingProcessId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public TrainingModule Module { get; set; } = new TrainingModule();
    }
}
