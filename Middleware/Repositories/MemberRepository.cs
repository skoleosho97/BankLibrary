using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        AppDbContext context;
        
        public MemberRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Member> FindByApplicant(Applicant applicant)
        {
            return await context.Members.FirstAsync(m => m.Applicant.Id == applicant.Id);
        }

        public async Task<Member> FindByMembershipId(string id)
        {
            return await context.Members.FirstOrDefaultAsync(m => m.MembershipId == id);
        }

        public async Task<bool> ExistsByApplicant(Applicant applicant)
        {
            return await context.Members.AnyAsync(m => m.Applicant.Id == applicant.Id);
        }
    }
}
