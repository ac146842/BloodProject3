using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloodProject3.Models
{
    public class Donor
    {
        [Key] //primary key
        [Display(Name = "Donor ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorID { get; set; }

        [Required(ErrorMessage = "First name is required.")] //required to be filled
        [StringLength(50)] //max 50 characters
        [Display(Name = "First Name")] // display as "First Name" in the UI
        [RegularExpression(@"^[a-zA-Z\s\-'āēīōūĀĒĪŌŪ]+$",
    ErrorMessage = "First name contains invalid characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")] //required to be filled
        [StringLength(50)] //max 50 characters 
        [Display(Name = "Last Name")] // display as "Last Name" in the UI
        [RegularExpression(@"^[a-zA-Z\s\-'āēīōūĀĒĪŌŪ]+$",
    ErrorMessage = "Last name contains invalid characters.")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Phone number is required.")] //required to be filled
        [StringLength(10)] //max 10 characters
        [Phone] //ensures input is a valid phone number format
        [Display(Name = "Phone Number")] // display as "Phone Number" in the UI
        [RegularExpression(@"^(0[2]\d{1}[\s\-]?\d{3}[\s\-]?\d{4}|0[3-9][\s\-]?\d{3}[\s\-]?\d{4})$",
    ErrorMessage = "Phone must be a valid NZ phone number (e.g., 021 123 4567 or 09 123 4567).")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")] //required to be filled
        [DataType(DataType.Date)] //ensures input is a valid date format
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Blood type is required.")] //fk to the bloodtype table and error message is given
        [ForeignKey("BloodTypeID")]
        [Display(Name = "Blood Type ID")]
        public int BloodTypeID { get; set; }
        public virtual BloodType? BloodType { get; set; }


        [DataType(DataType.Date)] //ensures input is a valid date format
        [Display(Name = "Last Donation Date")] // display as "Last Donation Date" in the UI
        public DateTime? LastDonationDate { get; set; }

        public virtual ICollection<DonatedBlood>? DonatedBloods { get; set; }

    }
}