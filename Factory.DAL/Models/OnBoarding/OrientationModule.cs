using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.OnBoarding
{
    public class OrientationModule
    {
        public int Id { get; set; }

        public int OnboardingProcessId { get; set; }
        public virtual OnboardingProcess OnboardingProcess { get; set; } = new OnboardingProcess();

        public bool CompanyOrientationCompleted { get; set; }
        public bool DepartmentOrientationCompleted { get; set; }
        public bool MentorAssigned { get; set; }
        public bool FirstWeekCheckInCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}