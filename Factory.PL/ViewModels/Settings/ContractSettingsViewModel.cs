using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.Settings
{
    public class ContractSettingsViewModel
    {
        [Required(ErrorMessage = "Contract start date is required.")]
        [Display(Name = "Contract Start Date")]
        [DataType(DataType.Date)]
        public DateTime ContractStartDate { get; set; }

        [Required(ErrorMessage = "Contract end date is required.")]
        [Display(Name = "Contract End Date")]
        [DataType(DataType.Date)]
        public DateTime ContractEndDate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}