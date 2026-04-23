using System.ComponentModel.DataAnnotations;

namespace BloodProject3.Models
{
    public class Questions
    {
        [Key]
        public int HealthQID { get; set; }

        [Required]
        public string FormQuestions { get; set; }
    }
}
