using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class MedicalForm
    {
        [Key]
        public int FormID { get; set; }

        [Required]
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }

        [Required]
        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        //add validation to ensure form date is not in the future
        public DateTime FormDate { get; set; }
    }
}
