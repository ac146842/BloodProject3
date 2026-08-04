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
        [Display(Name = "Form ID")]
        [ForeignKey("FormID")] // foreign key to MedicalForm
        public int FormID { get; set; }

        [Display(Name = "Questions")]
        //remove fkhealthqID
        [ForeignKey("HealthQID")]  //foreign key to HealthQuestions 
        public int HealthQID { get; set; }

        [ForeignKey("HealthQID")] 
        public virtual Questions Questions { get; set; }

        [ForeignKey("DonorID")] 
        [Display(Name = "Donor ID")]
        public int DonorID { get; set; }

        [ForeignKey("DonorID")] 
        public virtual Donor Donor { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Answer is required")] //makes sure an answer is provided and gives error message if no answer is given
        public string AnswersText { get; set; }

        [Required] //makes sure an answer is provided      
        [Display(Name = "Answer Date")]
        [NoFutureDate(ErrorMessage = "Answer date cannot be in the future.")]
        public DateTime AnswerDate { get; set; } = DateTime.Now;
    }
}