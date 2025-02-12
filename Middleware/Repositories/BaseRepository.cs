using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<T> FindById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> FindAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task Remove(T model)
        {
            context.Set<T>().Remove(model);
            await context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Save(T model)
        {
            await context.Set<T>().AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task Save(List<T> model)
        {
            await context.Set<T>().AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }
}
