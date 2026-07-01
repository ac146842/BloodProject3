using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Appointment
    {
        //check for multiple appointments during the same time
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }

        // DonorID is required, as every appointment must be associated with a donor
        [ForeignKey("DonorID")]
        public int DonorID { get; set; }
        public virtual Donor Donor { get; set; }

        // NurseID is required, as every appointment must be associated with a nurse and done in person
        [Required(ErrorMessage = "A nurse must be assigned.")]
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }
        public virtual Nurse Nurse { get; set; } // Required for .Include(a => a.Nurse)

        [Required(ErrorMessage = "Please select a date and time.")] //requires a date and time to be chosen and ensures date cannot be in the past with an error message
        [DataType(DataType.DateTime)]
        [NoPastDate(ErrorMessage = "The appointment date and time cannot be in the past.")] //custom validation attribute to check for past dates
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

        [Required(ErrorMessage = "Appointment duration is required.")]
        [Range(1, 60, ErrorMessage = "Appointment duration must be between 1 and 60 minutes.")]
        [Display(Name = "Duration (Minutes)")]
        public int DurationEndTime { get; set; }
    }
}