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
            return new ApplicationResponse
            {
                Id = application.Id,
                //ApplicationType = application.ApplicationType,
                //ApplicationStatus = application.ApplicationStatus,
                //PrimaryApplicant = application.PrimaryApplicant,
                //Applicants = application.Applicants,
            };
        }
    }
}
