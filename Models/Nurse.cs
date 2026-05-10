using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Nurse
    {
        [Key] 
        public int NurseID { get; set; }

        [Required(ErrorMessage = "Nurse must be linked to a User account.")]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please specify the nurse's job role.")]
        [StringLength(50)] //max string length of 50 characters
        [Display(Name = "Job Role")]
        public string JobRole { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Employment Start Date")]
        //add validation in controller
        public DateTime EmployedStartDate { get; set; }

        [Required(ErrorMessage = "License Number is required.")]
        [StringLength(8)] //max string length of 50 characters
        [Display(Name = "License Number")]
        [RegularExpression(@"^[A-Z]{2}\d{6}$", ErrorMessage = "License must be 2 uppercase letters followed by 6 digits.")]
        public string LicenseNumber { get; set; }
    }
}
