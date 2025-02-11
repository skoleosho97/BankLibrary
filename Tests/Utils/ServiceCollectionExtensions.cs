using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Data;

namespace Tests.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveDbContext<TContext>(this IServiceCollection services)
        {
            var context = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<AppDbContext>));
            
            if (context is not null)
                services.Remove(context);
        }

        public static void InitDbContext<TContext>(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();

            MockDataGenerator.Initialize(context);
        }
    }
}
