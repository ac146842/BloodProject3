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

        [Key] //primary key
        public int DonationID { get; set; }

        [Required] //required appointment id and fk
        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        [Required]
        [ForeignKey("BloodTypeID")] //required bloodtype id and fk
        public int BloodTypeID { get; set; }

        [Required]
        [ForeignKey("DonorID")] //required donor id and fk
        public int DonorID { get; set; }

        [Required(ErrorMessage = "Collection date is required")] //required collectiondate
        [DataType(DataType.Date)] //ensures ui shows a date pickter
        [Display(Name = "Date Collected")] //displays name on webpage as date collected
        public DateTime CollectionDate { get; set; }

        [Required(ErrorMessage = "Please enter the volume")] //required volume amount
        [Column(TypeName = "decimal(6, 2)")] // Formats the decimal for the Database with max number length being 5
        [Range(0.01, 500.00, ErrorMessage = "Volume must be between 0.01 and 500 ML")] //ensures volume is between 0.01 and 500
        [Display(Name = "Volume (ML)")] //displays name as Volume ML
        public decimal VolumeML { get; set; }

        [DataType(DataType.Date)] 
        [Display(Name = "Expiry Date")] //displays as Expiry date on web page
        // Collectiondate + 42 days
        public DateTime ExpiryDate { get; set; }

        [Required] //required field
        public Status BloodStatus { get; set; }
    }
}
