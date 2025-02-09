using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos.Responses;
using Core.Models;

namespace Core.Mappers
{
    public static class ApplicationMapper
    {
        public static ApplicationResponse ToApplicationResponse(this Application application)
        {
            List<ApplicantResponse> results = application.Applicants.Select(result => result.ToApplicantResponse()).ToList();

            return new ApplicationResponse
            {
                Id = application.Id,
                ApplicationType = application.ApplicationType,
                ApplicationStatus = application.ApplicationStatus,
                Applicants = results,
                PrimaryApplicant = application.PrimaryApplicant,
            };
        }

        public static ApplyResponse ToApplyResponse(this Application application)
        {
            List<ApplicantResponse> results = results = application.Applicants.Select(result => result.ToApplicantResponse()).ToList();

            return new ApplyResponse
            {
                Id = application.Id,
                ApplicationType = application.ApplicationType,
                Applicants = results,
                ApplicationStatus = application.ApplicationStatus
            };
        }
    }
}
