using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Donor
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorID { get; set; }

        [Required(ErrorMessage = "User link is required.")] //links donor back to a specific account
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Blood type is required.")] //fk to the bloodtype table and error message is given
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }
    }
}
