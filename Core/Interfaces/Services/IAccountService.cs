using Core.Models;
using Core.Models.Accounts;

namespace Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<List<Account>> CreateAccount(Application application, Member primary, List<Member> members);
        Task<Account> CreateCheckingAccount(Member primary, List<Member> members);
        Task<Account> CreateSavingsAccount(Member primary, List<Member> members);
        Task<Account> GetAccountByAccountNumber(string number);
    }
}
