using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Inventory
    {
        public enum Status
        {
            Available = 1,
            OnTheWay = 2,
            Used = 3,
            Expired = 4,
            Reserved = 5
        }

        [Key] //primary key
        public int BloodBankID { get; set; }

        //fk and required field
        [Required]
        [ForeignKey("DonationID")]
        public int DonationID { get; set; }

        //fk and required field
        [Required]
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }

        [Required(ErrorMessage = "Volume is required")] //required field with error message
        [Display(Name = "Current Volume (ML)")] //displays on web page as current volume
        //3 total digits, 2 after decimal
        [Column(TypeName = "decimal(3, 2)")]
        public decimal CurrentVolumeML { get; set; }

        [Required] //required
        [StringLength(15, ErrorMessage = "Location code cannot exceed 15 characters")] //max string length can be 15 with error message
        [Display(Name = "Storage Location")] //displays on web page as storage location
        //'Fridge-A1', 'Shelf-04'
        public string StorageLocation { get; set; }

        [Required] //required
        [Display(Name = "Inventory Status")] //displays as inventory status
        public Status BloodStatus { get; set; }
    }
}
