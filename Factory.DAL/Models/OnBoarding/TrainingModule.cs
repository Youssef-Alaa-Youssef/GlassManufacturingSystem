using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.OnBoarding
{
    public class TrainingModule
    {
        public int Id { get; set; }

        public int OnboardingProcessId { get; set; }
        public virtual OnboardingProcess OnboardingProcess { get; set; } = new OnboardingProcess();

        public bool ComplianceTraining { get; set; }
        public bool SkillsTraining { get; set; }
        public bool SystemsTraining { get; set; }
        public bool SecurityTraining { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}