using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Requests
{
    public class CreateApplicantRequest
    {    
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string SocialSecurity { get; set; } = string.Empty;
        [Required]
        public string DriversLicense { get; set; } = string.Empty;
        [Required]
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
    }
}
