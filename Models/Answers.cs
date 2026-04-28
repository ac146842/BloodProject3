using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Answers
    {
        [Key] //primary key
        public int AnswersID { get; set; }

        [Required]
        [ForeignKey("FormID")]
        public int FormID { get; set; }

        [ForeignKey("HealthQID")]  //foreign key to HealthQuestions
        public int HealthQID { get; set; }

        [ForeignKey("AppointmentID")] //foreign key to Appointments
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Answer is required")] //makes sure an answer is provided and gives error message if no answer is given
        public string QuestionAnswers { get; set; }

        public Answers() 
        { 
            AnswerDate = DateTime.Now; //defaults to current date and time, records time and date answers were written
        }

        [Required] //makes sure an answer is provided      
        public DateTime AnswerDate { get; set; }
    }
}
