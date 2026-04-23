using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject2.Models
{
    public class Answers
    {
        [Key]
        public int AnswersID { get; set; }

        [ForeignKey("HealthQID")]
        public int HealthQID { get; set; }

        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        [Required]
        public string QuestionAnswers { get; set; }

        [Required]
        //just the date
        //add validation to ensure answer date is not in the future
        public DateTime AnswerDate { get; set; }
    }
}
