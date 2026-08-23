using BloodProject3.Validation;
using System;
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }

        [Display(Name = "Donor ID")]
        public int DonorID { get; set; }

        [ForeignKey("DonorID")]
        public virtual Donor Donor { get; set; }

        [Required(ErrorMessage = "A nurse must be assigned.")]
        [Display(Name = "Nurse ID")]
        public int NurseID { get; set; }

        [ForeignKey("NurseID")]
        public virtual Nurse Nurse { get; set; }

        [Required(ErrorMessage = "Please select a date and time.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date & Time")]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Location")]
        [RegularExpression(@"^[a-zA-Z0-9\s,']+$", ErrorMessage = "Location contains invalid characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select the type of appointment.")]
        [Display(Name = "Type of Appointment")]
        public AppointmentType TypeOfAppointment { get; set; }

        [Required(ErrorMessage = "Please select a status for the appointment.")]
        [Display(Name = "Appointment Status")]
        public Status AppointmentStatus { get; set; }

        [Required(ErrorMessage = "Appointment duration is required.")]
        [Range(1, 60, ErrorMessage = "Appointment duration must be between 1 and 60 minutes.")]
        [Display(Name = "Duration (Minutes)")]
        public int DurationEndTime { get; set; }
    }
}