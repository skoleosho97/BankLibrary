using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Core.Models;
using Middleware.Data;

namespace Middleware.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        AppDbContext context;

        public ApplicationRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }        
    }
}
