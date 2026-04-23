using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class DonatedBlood
    {
        public enum Status
        {
            Pending = 1,
            Quarantined = 2,
            Approved = 3,
            Discarded = 4,
            Sent = 5
        }

        [Key]
        public int DonationID { get; set; }

        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }

        [ForeignKey("DonorID")]
        public int DonorID { get; set; }

        [Required]
        //just the date
        //add validation to ensure collection date is not in the future
        public DateTime CollectionDate { get; set; }

        [Required]
        //make decimal annotation
        public decimal VolumeML { get; set; }

        [Required]
        //just the date
        //add validation to ensure expiry date is after collection date
        public DateTime ExpiryDate { get; set; }

        public Status BloodStatus { get; set; }
    }
}
