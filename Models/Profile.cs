using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Profile
    {
        [Key]
        public int ProfileID { get; set; }

        [ForeignKey("UserID")]
        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //add validation to ensure date of birth is not in the future
        public DateTime DateOfBirth { get; set; }
    }
}
