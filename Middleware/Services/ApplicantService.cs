using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Exceptions.Conflict;
using Core.Exceptions.NotFound;
using Core.Mappers;
using Core.Models;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Middleware.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository repository;

        public ApplicantService(IApplicantRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ApplicantResponse> CreateApplicant(CreateApplicantRequest request)
        {
            Applicant applicant = request.ToCreateApplicant();
            await Validate(request.Email, request.Phone, request.SocialSecurity, request.DriversLicense);
            await repository.Save(applicant);

            return applicant.ToApplicantResponse();
        }

        public async Task<ApplicantResponse> GetApplicantById(int id)
        {
            Applicant applicant = await repository.FindById(id) ?? throw new ApplicantNotFoundException();

            return applicant.ToApplicantResponse();
        }

        public async Task<IEnumerable<ApplicantResponse>> GetApplicants()
        {
            List<Applicant> applicants = await repository.FindAll();
            IEnumerable<ApplicantResponse> response = applicants.Select(a => a.ToApplicantResponse());

            return response;
        }

        public async void UpdateApplicant(int id, UpdateApplicantRequest request)
        {
            await Validate(request.Email, request.Phone, request.SocialSecurity, request.DriversLicense);
            Applicant applicant = await repository.FindById(id) ?? throw new ApplicantNotFoundException();
            applicant.ToUpdateApplicant(request);
            await repository.Save();
        }

        public async void DeleteApplicant(int id)
        {
            Applicant applicant = await repository.FindById(id) ?? throw new ApplicantNotFoundException();
            await repository.Remove(applicant);
        }

        public async Task Validate(string? email, string? phone, string? socialSecurity, string? driversLicense)
        {
            if (await repository.DoesEmailExist(email) && email is not null)
                throw new ConflictException("Email is already in use by another applicant.");
            if (await repository.DoesPhoneExist(phone) && phone is not null)
                throw new ConflictException("Phone number is already in use by another applicant.");
            if (await repository.DoesSocialSecurityExist(socialSecurity) && socialSecurity is not null)
                throw new ConflictException("Social security number is already in use by another applicant.");
            if (await repository.DoesDriversLicenseExist(driversLicense) && driversLicense is not null)
                throw new ConflictException("Drivers license information is already in use by another applicant.");
        }
    }
}
