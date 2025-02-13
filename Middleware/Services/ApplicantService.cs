using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Mappers;
using Core.Models;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Exceptions;

namespace Middleware.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository repository;

        public ApplicantService(IApplicantRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Asynchronous operation that creates an applicant
        /// and saves it to the repository.
        /// </summary>
        /// <param name="request">DTO that contains request information.</param>
        /// <example>
        /// Example request:
        /// <code>
        /// {
        ///     FirstName = "John",
        ///     MiddleName = "Andrew", // Optional parameter
        ///     LastName = "Doe",
        ///     DateOfBirth = "1950/10/01",
        ///     Gender = "Male",
        ///     Email = "john.doe@gmail.com",
        ///     Phone = "1234567890",
        ///     SocialSecurity = "12345678",
        ///     DriversLicense = "",
        ///     Income = 100000,
        ///     Address = "123 First Street",
        ///     City = "New York City",
        ///     State = "NY",
        ///     Zipcode = "23412",
        ///     MAddress = "123 First Street",
        ///     MCity = "New York City",
        ///     MState = "NY",
        ///     MZipcode = "23412",
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <c>ApplicantResponse</c> DTO representing the
        /// newly created applicant.
        /// </returns>
        public async Task<ApplicantResponse> CreateApplicant(CreateApplicantRequest request)
        {
            Applicant applicant = request.ToCreateApplicant();
            await Validate(request.Email, request.Phone, request.SocialSecurity, request.DriversLicense);
            await repository.Save(applicant);

            return applicant.ToApplicantResponse();
        }

        /// <summary>
        /// Asynchronous operation that creates an applicant
        /// and saves it to the repository.
        /// </summary>
        /// <param name="request">DTO that contains request information.</param>
        /// <example>
        /// Example request:
        /// <code>
        /// {
        ///     FirstName = "John",
        ///     MiddleName = "Andrew", // Optional parameter
        ///     LastName = "Doe",
        ///     DateOfBirth = "1950/10/01",
        ///     Gender = "Male",
        ///     Email = "john.doe@gmail.com",
        ///     Phone = "1234567890",
        ///     SocialSecurity = "12345678",
        ///     DriversLicense = "",
        ///     Income = 100000,
        ///     Address = "123 First Street",
        ///     City = "New York City",
        ///     State = "NY",
        ///     Zipcode = "23412",
        ///     MAddress = "123 First Street",
        ///     MCity = "New York City",
        ///     MState = "NY",
        ///     MZipcode = "23412",
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <c>Applicant</c> object that was newly created.
        /// </returns>
        public async Task<Applicant> CreateApplicantForApplication(CreateApplicantRequest request)
        {
            Applicant applicant = request.ToCreateApplicant();
            await Validate(request.Email, request.Phone, request.SocialSecurity, request.DriversLicense);
            await repository.Save(applicant);

            return applicant;
        }

        /// <summary>
        /// Asynchronous operation that retrieves an applicant
        /// from the repository.
        /// </summary>
        /// <param name="id">Primary key integer that represents the requested applicant.</param>
        /// <returns>
        /// The requested <c>Applicant</c> object.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the applicant cannot be found.</exception>
        public async Task<Applicant> GetApplicantById(int id)
        {
            Applicant applicant = await repository.FindById(id) ?? throw new NotFoundException("Applicant was not found.");

            return applicant;
        }


        /// <summary>
        /// Asynchronous operation that retrieves an applicant
        /// from the repository.
        /// </summary>
        /// <param name="id">Primary key integer that represents the requested applicant.</param>
        /// <returns>
        /// The requested <c>Applicant</c> object mapped to a <c>ApplicantResponse</c> DTO.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the applicant cannot be found.</exception>
        public async Task<ApplicantResponse> GetApplicantResponseById(int id)
        {
            Applicant applicant = await repository.FindById(id) ?? throw new NotFoundException("Applicant was not found.");

            return applicant.ToApplicantResponse();
        }

        /// <summary>
        /// Asynchronous operation that retrieves all applicants
        /// from the repository.
        /// </summary>
        /// <returns>
        /// A iterable list of <c>Applicant</c> objects mapped to <c>ApplicantResponse</c> DTOs.
        /// </returns>
        public async Task<IEnumerable<ApplicantResponse>> GetApplicants()
        {
            List<Applicant> applicants = await repository.FindAll();
            IEnumerable<ApplicantResponse> response = applicants.Select(a => a.ToApplicantResponse());

            return response;
        }

        /// <summary>
        /// Asynchronous operation that updates an applicant.
        /// </summary>
        /// <param name="id">Primary key integer that represents the requested applicant.</param>
        /// <param name="request">DTO that contains request information:</param>
        /// <example>
        /// Example request:
        /// <code>
        /// {
        ///     // Only include parameters that you want to update
        ///     FirstName = "Mike",
        ///     Phone = "1236540987",
        ///     Zipcode = "23412",
        /// }
        /// </code>
        /// </example>
        /// <exception cref="NotFoundException">Thrown when the applicant cannot be found.</exception>
        public async void UpdateApplicant(int id, UpdateApplicantRequest request)
        {
            await Validate(request.Email, request.Phone, request.SocialSecurity, request.DriversLicense);
            Applicant applicant = await repository.FindById(id) ?? throw new NotFoundException("Applicant was not found.");
            applicant.ToUpdateApplicant(request);
            await repository.Save();
        }

        /// <summary>
        /// Asynchronous operation that deletes an applicant
        /// from the repository.
        /// </summary>
        /// <param name="id">Primary key integer that represents the requested applicant.</param>
        /// <exception cref="NotFoundException">Thrown when the applicant cannot be found.</exception>
        public async void DeleteApplicant(int id)
        {
            Applicant applicant = await repository.FindById(id) ?? throw new NotFoundException("Applicant was not found.");
            await repository.Remove(applicant);
        }

        /// <summary>
        /// Asynchronous operation that checks for uniqueness among
        /// specific properties.
        /// </summary>
        /// <param name="email">Email property to compare.</param>
        /// <param name="phone">Phone property to compare.</param>
        /// <param name="socialSecurity">Social security property to compare.</param>
        /// <param name="driversLicense">Social security property to compare.</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Thrown is any given property conflicts with
        // an already existing property in the repository.</exception>
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
