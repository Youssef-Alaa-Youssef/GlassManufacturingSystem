using Factory.DAL.Models.OnBoarding;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class ITSetupViewModel
    {
        public int OnboardingProcessId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public ITSetupModule Module { get; set; } = new ITSetupModule();
    }
}
