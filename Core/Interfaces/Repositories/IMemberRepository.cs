using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IMemberRepository : IRepository<Member> 
    { 
        Task<Member> FindByApplicant(Applicant applicant);
        Task<Member> FindByMembershipId(string id);
        Task<bool> ExistsByApplicant(Applicant applicant);
    }
}
