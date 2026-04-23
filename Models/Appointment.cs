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
        public int AppointmentID { get; set; }

        [Required]
        [ForeignKey("DonorID")]
        public int DonorID { get; set; }

        [Required]
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        //add validation to ensure appointment date and time is not in the past
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public int AppointmentTimes { get; set; }

        [Required]
        public AppointmentType TypeOfAppointment { get; set; }

        [Required]
        public Status AppointmentStatus { get; set; }
    }
}
