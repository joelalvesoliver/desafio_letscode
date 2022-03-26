using System.ComponentModel.DataAnnotations;

namespace Lets.Code.Application.Shared.Validations
{
    public class TitleValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            string.IsNullOrEmpty(value?.ToString()) ?
            new ValidationResult("Invalid title") :
            ValidationResult.Success;
    }
}
