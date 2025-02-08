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
        Task<ApplicationResponse> Apply(CreateApplicationRequest request);
        Task<List<Applicant>> CreateApplicants(List<CreateApplicantRequest> requests);
    }
}
