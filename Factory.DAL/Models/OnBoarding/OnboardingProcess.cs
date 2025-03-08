using Factory.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.OnBoarding
{
    public class OnboardingProcess
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public OnboardingStatus Status { get; set; }

        public virtual PreboardingModule Preboarding { get; set; } = new PreboardingModule();
        public virtual ITSetupModule ITSetup { get; set; } = new ITSetupModule();
        public virtual TrainingModule Training { get; set; } = new TrainingModule();
        public virtual OrientationModule Orientation { get; set; } = new OrientationModule();
    }

}