using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.OnBoarding
{
    public class PreboardingModule
    {
        public int Id { get; set; }

        public int OnboardingProcessId { get; set; }
        public virtual OnboardingProcess OnboardingProcess { get; set; } = new OnboardingProcess();

        public bool DocumentsReceived { get; set; }
        public bool ContractSigned { get; set; }
        public bool BackgroundCheckCompleted { get; set; }
        public bool WelcomeEmailSent { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}