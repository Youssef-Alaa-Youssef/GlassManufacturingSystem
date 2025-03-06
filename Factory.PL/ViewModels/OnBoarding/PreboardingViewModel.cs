using Factory.DAL.Models.OnBoarding;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class PreboardingViewModel
    {
        public int OnboardingProcessId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public PreboardingModule Module { get; set; } = new PreboardingModule();
    }
}
