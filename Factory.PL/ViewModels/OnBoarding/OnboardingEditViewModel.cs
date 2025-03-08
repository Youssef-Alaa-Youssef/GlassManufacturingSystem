using Factory.DAL.Enums;
using Factory.DAL.Models.OnBoarding;
using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class OnboardingEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Status")]
        public OnboardingStatus Status { get; set; }

        public PreboardingModule Preboarding { get; set; } = new PreboardingModule();
        public ITSetupModule ITSetup { get; set; } = new ITSetupModule();
        public TrainingModule Training { get; set; } = new TrainingModule();
        public OrientationModule Orientation { get; set; } = new OrientationModule();
    }
}
