using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodProject3.Models
{
    public class BloodType
    {
        public enum BloodGroup //display name is whats display to the user on the web page
        {
            [Display(Name = "A+")] APositive,
            [Display(Name = "A-")] ANegative,
            [Display(Name = "B+")] BPositive,
            [Display(Name = "B-")] BNegative,
            [Display(Name = "O+")] OPositive,
            [Display(Name = "O-")] ONegative,
            [Display(Name = "AB+")] ABPositive,
            [Display(Name = "AB-")] ABNegative
        }

        [Key] //primary key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BloodTypeID { get; set; }

        [Required] //required field
        //invalid selected column name selectedbloodtype
        public BloodGroup SelectedBloodType { get; set; }
    }
}