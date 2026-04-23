using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Donor
    {
        [Key]
        public int DonorID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required]
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }

        [DataType(DataType.Date)]
        //add validation to make sure donationdate isnt in future
        public DateTime? LastDonationDate { get; set; }
    }
}
