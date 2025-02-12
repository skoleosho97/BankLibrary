using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;

namespace Middleware.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository repository;

        public MemberService(IMemberRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Member> CreateMember(Applicant applicant)
        {
            bool exists = await repository.ExistsByApplicant(applicant);

            if (exists)
                return await repository.FindByApplicant(applicant);

            Member member = new()
            {
                Applicant = applicant
            };
            await repository.Save(member);
            
            return member;
        }

        public async Task<Member> GetMemberByMembershipId(string id)
        {
            return await repository.FindByMembershipId(id) ?? throw new NotFoundException("Requested member was not found.");
        }

        public async Task SaveAllMembers(List<Member> members)
        {
            await repository.Save(members);
        }
    }
}
