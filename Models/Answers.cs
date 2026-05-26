using BloodProject3.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloodProject3.Models
{
    public class Answers
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswersID { get; set; }

        [Required]
        [ForeignKey("FormID")] // foreign key to MedicalForm added to match ERD
        public int FormID { get; set; }

        [Display(Name = "Questions")]
        [ForeignKey("HealthQID")]  //foreign key to HealthQuestions
        public int HealthQID { get; set; }

        [ForeignKey("HealthQID")] // Fixed: Maps relationship directly to the existing HealthQID field above to prevent migration conflicts
        public virtual Questions Questions { get; set; }

        [ForeignKey("DonorID")] // foreign key updated from AppointmentID to DonorID to match new ERD map layout
        public int DonorID { get; set; }

        [ForeignKey("DonorID")] // Fixed: Added explicit foreign key mapping attribute to connect securely with the Donor data property
        public virtual Donor Donor { get; set; }

        [Required(ErrorMessage = "Answer is required")] //makes sure an answer is provided and gives error message if no answer is given
        public string AnswersText { get; set; } // Renamed from QuestionAnswers to match column naming convention on ERD layout view

        [Required] //makes sure an answer is provided      
        [NoFutureDate(ErrorMessage = "Answer date cannot be in the future.")]
        public DateTime AnswerDate { get; set; } = DateTime.Now;
    }
}