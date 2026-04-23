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

        [Key]
        public int BloodBankID { get; set; }

        [ForeignKey("DonationID")]
        public int DonationID { get; set; }

        [ForeignKey("BloodTypeID")]
        public int BloodTypeID { get; set; }

        //make decmial annotation
        public decimal CurrentVolumeML { get; set; }

        [StringLength(15)]
        public string StorageLocation { get; set; }

        public Status BloodStatus { get; set; }
    }
}
