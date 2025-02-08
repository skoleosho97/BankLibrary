using System.Globalization;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Models;

namespace Core.Mappers
{
    public static class ApplicantMapper
    {
        public static ApplicantResponse ToApplicantResponse(this Applicant applicant)
        {
            return new ApplicantResponse
            {
                Id = applicant.Id,
                FirstName = applicant.FirstName,
                MiddleName = applicant.MiddleName,
                LastName = applicant.LastName,
                DateOfBirth = applicant.DateOfBirth,
                Gender = applicant.Gender,
                Email = applicant.Email,
                Phone = applicant.Phone,
                SocialSecurity = applicant.SocialSecurity,
                DriversLicense = applicant.DriversLicense,
                Income = applicant.Income,
                Address = applicant.Address,
                City = applicant.City,
                State = applicant.State,
                Zipcode = applicant.Zipcode,
                MAddress = applicant.MAddress,
                MCity = applicant.MCity,
                MState = applicant.MState,
                MZipcode = applicant.MZipcode
            };
        }

        public static Applicant ToApplicant(this ApplicantResponse response)
        {
            return new Applicant
            {
                FirstName = response.FirstName,
                MiddleName = response.MiddleName,
                LastName = response.LastName,
                DateOfBirth = response.DateOfBirth,
                Gender = response.Gender,
                Email = response.Email,
                Phone = response.Phone,
                SocialSecurity = response.SocialSecurity,
                DriversLicense = response.DriversLicense,
                Income = response.Income,
                Address = response.Address,
                City = response.City,
                State = response.State,
                Zipcode = response.Zipcode,
                MAddress = response.MAddress,
                MCity = response.MCity,
                MState = response.MState,
                MZipcode = response.MZipcode,
            };                 
        }

        public static Applicant ToCreateApplicant(this CreateApplicantRequest request)
        {
            return new Applicant
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                Phone = request.Phone,
                SocialSecurity = request.SocialSecurity,
                DriversLicense = request.DriversLicense,
                Income = request.Income,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zipcode = request.Zipcode,
                MAddress = request.MAddress,
                MCity = request.MCity,
                MState = request.MState,
                MZipcode = request.MZipcode,
            };                    
        }

        public static void ToUpdateApplicant(this Applicant applicant, UpdateApplicantRequest request)
        {
            applicant.FirstName = request.FirstName is null ? applicant.FirstName : request.FirstName;
            applicant.MiddleName = request.MiddleName is null ? applicant.MiddleName : request.MiddleName;
            applicant.LastName = request.LastName is null ? applicant.LastName : request.LastName;
            applicant.DateOfBirth = request.DateOfBirth is null ? applicant.DateOfBirth : (DateOnly)request.DateOfBirth;
            applicant.Gender = request.Gender is null ? applicant.Gender : request.Gender;
            applicant.Email = request.Email is null ? applicant.Email : request.Email;
            applicant.Phone = request.Phone is null ? applicant.Phone : request.Phone;
            applicant.SocialSecurity = request.SocialSecurity is null ? applicant.SocialSecurity : request.SocialSecurity;
            applicant.DriversLicense = request.DriversLicense is null ? applicant.DriversLicense : request.DriversLicense;
            applicant.Income = request.Income is null ? applicant.Income : (int)request.Income;
            applicant.Address = request.Address is null ? applicant.Address : request.Address;
            applicant.City = request.City is null ? applicant.City : request.City;
            applicant.State = request.State is null ? applicant.State : request.State;
            applicant.Zipcode = request.Zipcode is null ? applicant.Zipcode : request.Zipcode;
            applicant.MAddress = request.MAddress is null ? applicant.MAddress : request.MAddress;
            applicant.MCity = request.MCity is null ? applicant.MCity : request.MCity;
            applicant.MState = request.MState is null ? applicant.MState : request.MState;
            applicant.MZipcode = request.MZipcode is null ? applicant.MZipcode : request.MZipcode;
        }
    }

}
