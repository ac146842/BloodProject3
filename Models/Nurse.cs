using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloodProject3.Models

{

    public class Nurse

    {

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int NurseID { get; set; }



        [Required(ErrorMessage = "First name is required.")]

        [StringLength(50)] //max 50 characters

        [Display(Name = "First Name")]

        public string FirstName { get; set; }



        [Required(ErrorMessage = "Last name is required.")]

        [StringLength(50)] //max 50 characters

        [Display(Name = "Last Name")]

        public string LastName { get; set; }



        [Required]

        [StringLength(15)] //max 15

        [Phone] //ensures input is a valid phone number format

        public string Phone { get; set; }

        [Required(ErrorMessage = "Please specify the nurse's job role.")]

        [StringLength(50)] //max string length of 50 characters

        [Display(Name = "Job Role")]

        public string JobRole { get; set; }



        [Required(ErrorMessage = "Start date is required.")]

        [DataType(DataType.Date)]

        [Display(Name = "Employment Start Date")]

        [NoPastDate(ErrorMessage = "Start date cannot be in the past.")] // Custom validation attribute to prevent past dates

        public DateTime EmployedStartDate { get; set; }



        [Required(ErrorMessage = "License Number is required.")]

        [StringLength(8)] //max string length of 50 characters

        [Display(Name = "License Number")]

        [RegularExpression(@"^[A-Z]{2}\d{6}$", ErrorMessage = "License must be 2 uppercase letters followed by 6 digits.")]

        public string LicenseNumber { get; set; }

    }
}