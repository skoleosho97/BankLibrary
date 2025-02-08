using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NameValidator : ValidationAttribute
    {
        public string Message { get; set; } = string.Empty;

        public NameValidator() {}
    
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            string name = (string)value;
            Regex regex = new Regex(@"^[A-Za-z][A-Za-z-']+$");
            Match match = regex.Match(name);

            if (!match.Success)
                return new ValidationResult(string.Format(Message, name));
            else
                return ValidationResult.Success;
        }
    }
}
