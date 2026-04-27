using System.ComponentModel.DataAnnotations;

namespace BloodProject3.Models
{
    public class Questions
    {
        [Key]
        public int HealthQID { get; set; }

        [Required(ErrorMessage = "Question text cannot be empty.")]
        [StringLength(500)]
        [Display(Name = "Question Text")]
        public string FormQuestions { get; set; }
    }
}
