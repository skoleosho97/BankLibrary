using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IMemberService
    {
        Task<Member> CreateMember(Applicant applicant);
        Task<Member> GetMemberByMembershipId(string id);
        Task SaveAllMembers(List<Member> members);
    }
}
