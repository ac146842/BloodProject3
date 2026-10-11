using System;
using System.Collections.Generic;
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
        [RegularExpression(@"^[a-zA-Z0-9\s\?\!\.\,\'\-\:\;\(\)\/āēīōūĀĒĪŌŪ]+$",
    ErrorMessage = "Question contains invalid characters.")]
        public string FormQuestions { get; set; }

        public virtual ICollection<Answers>? Answers { get; set; }
    }
}