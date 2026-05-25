using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    // C#
    public class MedicalForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FormID { get; set; }

        [Required]
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }
        public virtual Nurse Nurse { get; set; }

        [Required]
        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; } //= new Appointment(); was problem , good for write up documentation on errors

        [Required]
        [DataType(DataType.DateTime)]
        [NoFutureDateAttribute(ErrorMessage = "Form date cannot be set in the future.")]
        public DateTime FormDate { get; set; }
    }
}