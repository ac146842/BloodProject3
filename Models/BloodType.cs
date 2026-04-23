using System.ComponentModel.DataAnnotations;

namespace BloodProject3.Models
{
    public class BloodType
    {
        [Key]
        public int BloodTypeID { get; set; }

        [Required]
        [StringLength(3)]
        public string BloodTypeName { get; set; }
    }
}
