using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IApplicationService
    {
        Task<Application> GetApplicationById(int id);
        Task<ApplicationResponse> GetApplicationResponseById(int id);
        void DeleteApplication(int id);
        Task<ApplyResponse> Apply(CreateApplicationRequest request);
        Task<IEnumerable<ApplicationResponse>> GetAllApplications();
        Task<List<Applicant>> CreateApplicants(List<CreateApplicantRequest> requests);
        Task<List<Applicant>> GetApplicants(List<int> requests);
    }
}
