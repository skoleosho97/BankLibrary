using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Validation;

namespace Core.Models
{
    public class Applicant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    
        [Required]
        [NameValidator(Message = "{0} is not a valid name.")]
        public string FirstName { get; set; } = string.Empty;

        [NameValidator(Message = "{0} is not a valid name.")]
        public string? MiddleName { get; set; }

        [Required]
        [NameValidator(Message = "{0} is not a valid name.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DateOfBirthValidator]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MinLength(10)]
        [MaxLength(10)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        public string SocialSecurity { get; set; } = string.Empty;

        [Required]
        public string DriversLicense { get; set; } = string.Empty;

        [Required]
        [IncomeValidator]
        public int Income { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string Zipcode { get; set; } = string.Empty;

        [Required]
        public string MAddress { get; set; } = string.Empty;

        [Required]
        public string MCity { get; set; } = string.Empty;

        [Required]
        public string MState { get; set; } = string.Empty;

        [Required]
        public string MZipcode { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModified { get; set; }
    
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        // public List<> Applications { get; set; }
    }

}
