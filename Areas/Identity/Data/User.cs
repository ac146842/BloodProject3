using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BloodProject3.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
