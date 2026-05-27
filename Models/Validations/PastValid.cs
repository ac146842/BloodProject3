using System.ComponentModel.DataAnnotations;

namespace BloodProject3.Validation
{
    public class NoPastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Date < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date cannot be in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }
}