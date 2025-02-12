using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Core.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext context;

        public AccountRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Account?> FindByAccountNumber(string number)
        {
            return await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == number);
        }
    }
}
