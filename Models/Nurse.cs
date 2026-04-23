using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Nurse
    {
        [Key]
        public int NurseID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string JobRole { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EmployedStartDate { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }
    }
}
