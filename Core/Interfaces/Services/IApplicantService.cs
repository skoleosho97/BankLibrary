using Core.Dtos.Requests;
using Core.Dtos.Responses;

namespace Core.Interfaces.Services
{
    public interface IApplicantService
    {
        Task<ApplicantResponse> CreateApplicant(CreateApplicantRequest request);
        Task<ApplicantResponse> GetApplicantById(int id);
        Task<IEnumerable<ApplicantResponse>> GetApplicants();
        void UpdateApplicant(int id, UpdateApplicantRequest request);
        void DeleteApplicant(int id);
        Task Validate(string? email, string? phone, string? socialSecurity, string? driversLicense);
    }
}
