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
        
        [Required(ErrorMessage = "First name is required.")] //required to be filled
        [StringLength(50)] //max 50 characters
        [Display(Name = "First Name")] // display as "First Name" in the UI
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")] //required to be filled
        [StringLength(50)] //max 50 characters 
        [Display(Name = "Last Name")] // display as "Last Name" in the UI
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Phone number is required.")] //required to be filled
        [StringLength(15)] //max 15 characters
        [Phone] //ensures input is a valid phone number format
        [Display(Name = "Phone Number")] // display as "Phone Number" in the UI
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in the format 000-000-0000")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")] //required to be filled
        [DataType(DataType.Date)] //ensures input is a valid date format
        [NoFutureDate(ErrorMessage = "Date of birth cannot be set in the future.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Blood type is required.")] //fk to the bloodtype table and error message is given
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }
        public virtual BloodType BloodType { get; set; }


        [DataType(DataType.Date)] //ensures input is a valid date format
        [Display(Name = "Last Donation Date")] // display as "Last Donation Date" in the UI
        [NoFutureDate(ErrorMessage = "Last donation date cannot be set in the future.")]
        public DateTime? LastDonationDate { get; set; }

        public virtual ICollection<DonatedBlood> DonatedBloods { get; set; }

    }
}