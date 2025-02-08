using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IncomeValidator : ValidationAttribute
    {
        public string Message { get; set; } = string.Empty;

        public IncomeValidator()
        {
            Message = "Income must be greater than 0.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            int income = (int)value;

            if (income <= 0)
                return new ValidationResult(string.Format(Message));
            else
                return ValidationResult.Success;
        }
    }
}
