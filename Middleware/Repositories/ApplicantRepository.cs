using Middleware.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Repositories;

namespace Middleware.Repositories
{
    public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
    {
        AppDbContext context;

        public ApplicantRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> DoesEmailExist(string? email)
        {
            return await context.Applicants.AnyAsync(a => a.Email == email);
        }

        public async Task<bool> DoesPhoneExist(string? phone)
        {
            return await context.Applicants.AnyAsync(a => a.Phone == phone);
        }

        public async Task<bool> DoesSocialSecurityExist(string? socialSecurity)
        {
            return await context.Applicants.AnyAsync(a => a.SocialSecurity == socialSecurity);
        }

        public async Task<bool> DoesDriversLicenseExist(string? driversLicense)
        {
            return await context.Applicants.AnyAsync(a => a.DriversLicense == driversLicense);
        }
    }

}
