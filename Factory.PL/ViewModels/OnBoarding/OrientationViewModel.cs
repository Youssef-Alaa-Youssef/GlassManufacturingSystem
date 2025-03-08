using Factory.DAL.Models.OnBoarding;

namespace Factory.PL.ViewModels.OnBoarding
{

    public class OrientationViewModel
    {
        public int OnboardingProcessId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public OrientationModule Module { get; set; } = new OrientationModule();
    }
}
