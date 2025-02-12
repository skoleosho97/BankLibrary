using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Accounts;

namespace Core.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> FindByAccountNumber(string number);
    }
}
