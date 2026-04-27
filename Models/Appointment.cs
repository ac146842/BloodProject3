using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Appointment
    {
        public enum AppointmentType
        {
            Consulting = 1,
            Donation = 2,
            Results = 3,
            CheckUp = 4
        }

        public enum Status
        {
            Scheduled = 1,
            Completed = 2,
            Cancelled = 3,
            PutOff = 4
        }

        [Key] //primary key
        public int AppointmentID { get; set; }

        // DonorID is required, as every appointment must be associated with a donor
        [ForeignKey("DonorID")]
        public int DonorID { get; set; }

        // NurseID is required, as every appointment must be associated with a nurse and done in person
        [Required(ErrorMessage = "A nurse must be assigned.")] 
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }

        [Required(ErrorMessage = "Please select a date and time.")] //requires a date and time to be chosen and ensures date cannot be in the past with an error message
        [DataType(DataType.DateTime)]
        
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)] //max location characters is 100 with a minimum of 3
        [RegularExpression(@"^[a-zA-Z0-9\s,']+$", ErrorMessage = "Location contains invalid characters.")] //Checks for any characters that doesnt match the ones inside the brackets with an error message if invalid input is given
        public string Location { get; set; }

        // TypeOfAppointment is required, as every appointment must have a type with an error message if not chosen
        [Required(ErrorMessage = "Please select the type of appointment.")]
        public AppointmentType TypeOfAppointment { get; set; }

        // Status is required, as every appointment must have a status with an error message if not chosen
        [Required(ErrorMessage = "Please select a status for the appointment.")]
        public Status AppointmentStatus { get; set; }
    }
}
