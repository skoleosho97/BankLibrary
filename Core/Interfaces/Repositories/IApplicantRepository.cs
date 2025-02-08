using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<bool> DoesEmailExist(string? email);
        Task<bool> DoesPhoneExist(string? phone);
        Task<bool> DoesSocialSecurityExist(string? socialSecurity);
        Task<bool> DoesDriversLicenseExist(string? driversLicense);
    }
}
