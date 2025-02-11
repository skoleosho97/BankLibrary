using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;
using Core.Models;

namespace Middleware.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository repository;
        private readonly IApplicantService applicantService;

        public ApplicationService(
            IApplicationRepository repository, 
            IApplicantService applicantService
        )
        {
            this.repository = repository;
            this.applicantService = applicantService;
        }

        public async Task<Application> GetApplicationById(int id)
        {
           Application application = await repository.FindById(id) ?? throw new NotFoundException("Requested application was not found.");

           return application;
        }

        public async Task<ApplicationResponse> GetApplicationResponseById(int id)
        {
           Application application = await repository.FindById(id) ?? throw new NotFoundException("Requested application was not found.");

           return application.ToApplicationResponse();
        }

        public async void DeleteApplication(int id)
        {
            Application application = await repository.FindById(id) ?? throw new NotFoundException("Requested application was not found.");
            await repository.Remove(application);
        }

        public async Task<ApplyResponse> Apply(CreateApplicationRequest request)
        {
            Application? application = null;

            if (!request.NoNewApplicants)
            {
                List<Applicant> applicants = await CreateApplicants(request.Applicants);
                Applicant primary = applicants.First();

                application = new Application {
                   PrimaryApplicant = primary,
                   Applicants = applicants,
                   ApplicationType = request.ApplicationType,
                   ApplicationStatus = "PENDING",
                   //ApplicationAmount = request.ApplicationAmount
                };
            }
            else
            {
                List<int> applicantIds = request.ApplicantIds ?? throw new BadRequestException(@"NoNewApplicants property was set to true
                                                                                                but ApplicantIds property was not found in the request.");

                if (applicantIds.Count is 0)
                    throw new BadRequestException("NoNewApplicants property was set to true but applicantIds property is empty.");

                List<Applicant> applicants = await GetApplicants(applicantIds);
                Applicant primary = applicants.First();

                application = new Application {
                   PrimaryApplicant = primary,
                   Applicants = applicants,
                   ApplicationType = request.ApplicationType,
                   ApplicationStatus = "PENDING",
                   //ApplicationAmount = request.ApplicationAmount
                };
            }

            if (application is null)
                throw new BadRequestException("Application request could not be processed.");

            if (application.ApplicationType == "LOAN")
            {
                // do loan stuff
            }

            if (application.ApplicationType == "CREDIT_CARD")
            {
                // do credit card stuff
            }

            await repository.Save();
            ApplyResponse response = application.ToApplyResponse();

            HelperService.ProcessApplication(application, 
                (status, reason) => 
                {
                    application.ApplicationStatus = status;
                    response.ApplicationStatus = status;
                    response.Reasons = reason;

                    if (status is not "APPROVED")
                        return;

                    // Create members
                    // Create accounts
                    // Set AccountsCreated, CreatedAccounts, MembersCreated, CreatedMembers
                }
            );

            return response;
        }

        public async Task<IEnumerable<ApplicationResponse>> GetAllApplications()
        {
            List<Application> applications = await repository.FindAll();
            IEnumerable<ApplicationResponse> responses = applications.Select(a => a.ToApplicationResponse());

            return responses;
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

        public async Task<List<Applicant>> GetApplicants(List<int> requests)
        {
            IEnumerable<Task<ApplicantResponse>> responses = requests.Select(async request => 
                await applicantService.GetApplicantById(request)
            );

            IEnumerable<ApplicantResponse> results = await Task.WhenAll(responses);

            IEnumerable<Applicant> applicants = results.Select(result => 
                result.ToApplicant()
            );

            return applicants.ToList();
        }
    }
}
