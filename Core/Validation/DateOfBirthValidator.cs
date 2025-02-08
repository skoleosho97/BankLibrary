using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateOfBirthValidator : ValidationAttribute
    {
        public int MinAge { get; set; }
        public string Message { get; set; } = string.Empty;

        public DateOfBirthValidator()
        {
            MinAge = 18;
            Message = "Minimum age to sign up is 18 years or older.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            DateTime birth = (DateTime)value;
            int age = DateTime.Now.Year - birth.Year;
            if (DateTime.Now.DayOfYear < birth.DayOfYear) age--;

            if (age < MinAge)
                return new ValidationResult(string.Format(Message));
            else
                return ValidationResult.Success;
        }
    }

}
