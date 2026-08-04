using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HealthQID { get; set; }

        [Required(ErrorMessage = "Question text cannot be empty.")]
        [StringLength(500)]
        [Display(Name = "Questions")]
        public string FormQuestions { get; set; }
    }
}