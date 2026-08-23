using BloodProject3.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class Answers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswersID { get; set; }

        [Required]
        [Display(Name = "Form ID")]
        public int FormID { get; set; }

        [Display(Name = "Questions")]
        public int HealthQID { get; set; }

        [ForeignKey("HealthQID")]
        public virtual Questions Questions { get; set; }

        [Display(Name = "Donor ID")]
        public int DonorID { get; set; }

        [ForeignKey("DonorID")]
        public virtual Donor Donor { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Answer is required")]
        public string AnswersText { get; set; }

        [Required]
        [Display(Name = "Answer Date")]
        [NoFutureDate(ErrorMessage = "Answer date cannot be in the future.")]
        public DateTime AnswerDate { get; set; } = DateTime.Now;
    }
}