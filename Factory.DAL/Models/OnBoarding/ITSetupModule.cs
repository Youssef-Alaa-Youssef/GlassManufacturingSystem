using System.ComponentModel.DataAnnotations;

namespace Factory.DAL.Models.OnBoarding
{
    public class ITSetupModule
    {
        public int Id { get; set; }

        public int OnboardingProcessId { get; set; }
        public virtual OnboardingProcess OnboardingProcess { get; set; }

        public bool EmailSetup { get; set; }
        public bool HardwareProvisioned { get; set; }
        public bool SoftwareInstalled { get; set; }
        public bool AccessGranted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        public string Notes { get; set; }
    }
}