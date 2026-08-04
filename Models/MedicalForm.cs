using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class MedicalForm
    {
        [Key]
        [Display(Name = "Form ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FormID { get; set; }

        [Required]
        [Display(Name = "Nurse ID")]
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }
        public virtual Nurse Nurse { get; set; }

        [Required]
        [Display(Name = "Appointment ID")]
        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; } //= new Appointment(); was problem ,caused by auto type vs thingy, good for write up documentation on errors

        [Required]
        [Display(Name = "Form Date")]
        [DataType(DataType.DateTime)]
        [NoFutureDateAttribute(ErrorMessage = "Form date cannot be set in the future.")]
        public DateTime FormDate { get; set; }
    }
}