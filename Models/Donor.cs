using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Donor
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorID { get; set; }

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
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in the format 000-000-0000")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [NoFutureDate(ErrorMessage = "Date of birth cannot be set in the future.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Blood type is required.")] //fk to the bloodtype table and error message is given
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }
        public virtual BloodType BloodType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Donation Date")]
        [NoFutureDate(ErrorMessage = "Last donation date cannot be set in the future.")]
        public DateTime? LastDonationDate { get; set; }
    }
}