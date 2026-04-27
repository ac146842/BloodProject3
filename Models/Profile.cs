using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Profile
    {
        [Key]
        public int ProfileID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)] //max 50 characters
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)] //max 50 characters 
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15)] //max 15
        [Phone] //ensures input is a valid phone number format
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //add validation
        public DateTime DateOfBirth { get; set; }
    }
}
