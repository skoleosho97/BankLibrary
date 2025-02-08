using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneValidator : ValidationAttribute
    {
        public string Message { get; set; } = string.Empty;

        public PhoneValidator()
        {
            Message = "Phone number must be no more than 10 digits long.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            string phone = (string)value;

            if (phone.Length < 10)
                return new ValidationResult(Message);
            else
                return ValidationResult.Success;
        }
    }
}
