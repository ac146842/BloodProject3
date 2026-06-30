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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BloodBankID { get; set; }

        //fk and required field
        [Required]
        [ForeignKey("DonationID")]
        public int DonationID { get; set; }

        //fk and required field
        [Required]
        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }
        public virtual BloodType BloodType { get; set; }

        [Required(ErrorMessage = "Volume is required")] //required field with error message
        [Display(Name = "Current Volume (ML)")] // display as "Last Donation Date" in the UI
        [Column(TypeName = "decimal(6, 2)")] //6 total digits, 2 after decimal
        public decimal CurrentVolumeML { get; set; }

        [Required(ErrorMessage = "Storage location is required.")] //required to be filled
        [StringLength(15, ErrorMessage = "Location code cannot exceed 15 characters")] //max string length can be 15 with error message
        [Display(Name = "Storage Location")] // display as "Storage Location" in the UI
        //'Fridge-A1', 'Shelf-04'
        public string StorageLocation { get; set; }

        [Required(ErrorMessage = "Inventory status is required.")] //required to be filled
        [Display(Name = "Inventory Status")] // display as "Inventory Status" in the UI
        public Status BloodStatus { get; set; }
    }
}