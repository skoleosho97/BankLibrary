namespace Core.Dtos.Responses
{
    public class ApplicantResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string SocialSecurity { get; set; } = string.Empty;
        public string DriversLicense { get; set; } = string.Empty;
        public int Income { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string MAddress { get; set; } = string.Empty;
        public string MCity { get; set; } = string.Empty;
        public string MState { get; set; } = string.Empty;
        public string MZipcode { get; set; } = string.Empty;
        public string LastModified { get; set; } = DateTime.Now.ToString();
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
    }

}
