using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;
using Core.Models;
using Core.Models.Accounts;

namespace Middleware.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository repository;
        private readonly IApplicantService applicantService;
        private readonly IMemberService memberService;
        private readonly IAccountService accountService;

        public ApplicationService(
            IApplicationRepository repository, 
            IApplicantService applicantService,
            IMemberService memberService,
            IAccountService accountService
        )
        {
            this.repository = repository;
            this.applicantService = applicantService;
            this.memberService = memberService;
            this.accountService = accountService;
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
                                                                                                but ApplicantIds property was not found
                                                                                                in the request.");

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

            await HelperService.ProcessApplication(application, 
                async (status, reason) => 
                {
                    application.ApplicationStatus = status;
                    response.ApplicationStatus = status;
                    response.Reasons = reason;

                    if (status is not "APPROVED")
                        return;

                    var membersTasks = application.Applicants.Select(async applicant =>
                        await memberService.CreateMember(applicant)
                    ).ToList();

                    var members = (await Task.WhenAll(membersTasks)).ToList();
                    Member primaryMember = members.First();

                    List<Account> accounts = await accountService.CreateAccount(application, primaryMember, members);

                    members.ForEach(member => member.Accounts = accounts);
                    await memberService.SaveAllMembers();

                    List<AccountResponse> createdAccounts = accounts.Select(account => 
                        new AccountResponse() { 
                            AccountNumber = account.AccountNumber,
                            AccountType = Enum.TryParse<AccountType>(
                                account.GetType().Name, 
                                out AccountType enumType) ? enumType : null }).ToList();
                    List<MemberResponse> createdMembers = members.Select(member =>
                        new MemberResponse() { 
                            MembershipId = member.MembershipId, 
                            Name = $"{member.Applicant.FirstName} {member.Applicant.MiddleName} {member.Applicant.LastName}" }).ToList();

                    response.AccountsCreated = true;
                    response.CreatedAccounts = createdAccounts;
                    response.MembersCreated = true;
                    response.CreatedMembers = createdMembers;
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
            var responses = requests.Select(async request => 
                await applicantService.CreateApplicant(request)
            ).ToList();

            var results = await Task.WhenAll(responses);

            IEnumerable<Applicant> applicants = results.Select(result => 
                result.ToApplicant()
            );

            return applicants.ToList();
        }

        public async Task<List<Applicant>> GetApplicants(List<int> requests)
        {
            var responses = requests.Select(async request => 
                await applicantService.GetApplicantById(request)
            ).ToList();

            var applicants = await Task.WhenAll(responses);

            return applicants.ToList();
        }
    }
}
