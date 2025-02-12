using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Accounts;

namespace Middleware.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;

        public AccountService(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Account>> CreateAccount(Application application, Member primary, List<Member> members)
        {
            List<Account> accounts = [];

            switch (application.ApplicationType)
            {
                case "CHECKING":
                    accounts.Add(await CreateCheckingAccount(primary, members));
                    break;
                case "SAVINGS":
                    accounts.Add(await CreateSavingsAccount(primary, members));
                    break;
                case "CHECKING_AND_SAVINGS":
                    accounts.Add(await CreateCheckingAccount(primary, members));
                    accounts.Add(await CreateSavingsAccount(primary, members));
                    break;
                default:
                    break;
            }

            return accounts;
        }

        public async Task<Account> CreateCheckingAccount(Member primary, List<Member> members)
        {
            CheckingAccount account = new()
            {
                Status = "ACTIVE",
                PrimaryAccountHolder = primary,
                Balance = 0,
                Members = members,
                AvailableBalance = 0
            };

            await repository.Save(account);

            return account;
        }

        public async Task<Account> CreateSavingsAccount(Member primary, List<Member> members)
        {
            SavingsAccount account = new()
            {
                Status = "ACTIVE",
                PrimaryAccountHolder = primary,
                Balance = 0,
                Members = members,
                Apy = 0.006f
            };

            await repository.Save(account);
            
            return account;
        }

        public async Task<Account> GetAccountByAccountNumber(string number)
        {
            return await repository.FindByAccountNumber(number) ?? throw new NotFoundException("Request account was not found.");
        }
    }
}
