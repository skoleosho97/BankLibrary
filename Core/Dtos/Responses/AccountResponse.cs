using Core.Models.Accounts;

namespace Core.Dtos.Responses
{
    public class AccountResponse
    {
        public string AccountNumber { get; set; } = string.Empty;

        public AccountType? AccountType { get; set; }
    }
}
