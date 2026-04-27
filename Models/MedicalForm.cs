using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class MedicalForm
    {
        [Key] //pk
        public int FormID { get; set; }

        [Required] //required 
        [ForeignKey("NurseID")] //fk
        public int NurseID { get; set; }

        [Required] //required
        [ForeignKey("AppointmentID")] //fk
        public int AppointmentID { get; set; }

        [Required] 
        [DataType(DataType.DateTime)] //makes a date time picker in the form
        //add validation in controller
        public DateTime FormDate { get; set; }
    }
}
