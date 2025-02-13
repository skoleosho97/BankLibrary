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
                    var account = await CreateCheckingAccount(primary, members);
                    accounts.Add(account);
                    break;
                case "SAVINGS":
                    break;
                case "CHECKING_AND_SAVINGS":
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

            account.AccountNumber = account.GenerateAccountNumber();

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

            account.AccountNumber = account.GenerateAccountNumber();

            await repository.Save(account);
            
            return account;
        }

        public async Task<Account> GetAccountByAccountNumber(string number)
        {
            return await repository.FindByAccountNumber(number) ?? throw new NotFoundException("Request account was not found.");
        }
    }
}
