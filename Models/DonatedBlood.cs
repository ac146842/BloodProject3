using BloodProject3.Validation;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationID { get; set; }

        [Required] //required appointment id and fk
        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }
        public virtual Appointment Appointment { get; set; }

        [Required]
        [ForeignKey("BloodTypeID")] //required bloodtype id and fk
        public int BloodTypeID { get; set; }
        public virtual BloodType BloodType { get; set; }

        [Required]
        [ForeignKey("DonorID")] //required donor id and fk
        public int DonorID { get; set; }
        public virtual Donor Donor { get; set; }

        [Required(ErrorMessage = "Collection date is required")] //required collectiondate
        [DataType(DataType.Date)] //ensures ui shows a date pickter
        [Display(Name = "Date Collected")] //displays name on webpage as date collected
        [NoFutureDateAttribute(ErrorMessage = "Collection Date cannot be in the future.")] //Need to change
        public DateTime CollectionDate { get; set; }

        [Required(ErrorMessage = "Please enter the volume")] //required volume amount
        [Column(TypeName = "decimal(6, 2)")] // Formats the decimal for the Database with max number length being 5
        [Range(0.01, 500.00, ErrorMessage = "Volume must be between 0.01 and 500 ML")] //ensures volume is between 0.01 and 500
        [Display(Name = "Volume (ML)")] //displays name as Volume ML
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal VolumeML { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Required] //required field
        public Status BloodStatus { get; set; }
    }
}