namespace Core.Dtos.Responses
{
    public class ApplyResponse
    {
        public int Id { get; set; }
        public string ApplicationType { get; set; } = string.Empty;
        // LoanType
        // ApplyAmount
        public List<ApplicantResponse> Applicants { get; set; } = [];
        public string ApplicationStatus { get; set; } = string.Empty;
        public List<string> Reasons { get; set; } = [];
        public bool AccountsCreated { get; set; }
        public List<AccountResponse> CreatedAccounts { get; set; } = [];
        public bool MembersCreated { get; set; }
        public List<MemberResponse> CreatedMembers { get; set; } = [];
    }
}
