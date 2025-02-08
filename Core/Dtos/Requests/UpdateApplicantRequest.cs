using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Requests
{
    public class UpdateApplicantRequest
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? SocialSecurity { get; set; }
        public string? DriversLicense { get; set; }
        public int? Income { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
        public string? MAddress { get; set; }
        public string? MCity { get; set; }
        public string? MState { get; set; }
        public string? MZipcode { get; set; }
        public DateTime LastModified { get; set; }
    }
}
