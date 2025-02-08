using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;
using Core.Models;

namespace Middleware.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository repository;
        private readonly ApplicantService applicantService;

        public ApplicationService(IApplicationRepository repository, ApplicantService applicantService)
        {
            this.repository = repository;
            this.applicantService = applicantService;
        }

        public async Task<ApplicationResponse> Apply(CreateApplicationRequest request)
        {
            Application? application = null;

            if (!request.NoNewApplicants)
            {
                List<Applicant> applicants = await CreateApplicants(request.Applicants);
                Applicant primary = applicants.First();

                application = new Application {
                    
                };
            }
            else
            {

            }

            if (application is null)
            {
                // throw exception of some kind
            }

            await repository.Save(application);
            ApplicationResponse response = application.ToApplicationResponse();

            return response;
        }

        public async Task<List<Applicant>> CreateApplicants(List<CreateApplicantRequest> requests)
        {
            IEnumerable<Task<ApplicantResponse>> responses = requests.Select(async request => 
                await applicantService.CreateApplicant(request)
            );

            IEnumerable<ApplicantResponse> results = await Task.WhenAll(responses);

            IEnumerable<Applicant> applicants = results.Select(result => 
                result.ToApplicant()
            );

            return applicants.ToList();
        }
    }
}
