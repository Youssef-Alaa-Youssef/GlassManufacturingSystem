using System.ComponentModel.DataAnnotations;

namespace Factory.PL.ViewModels.OnBoarding
{
    public class OnboardingCreateViewModel
    {
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
    }











}