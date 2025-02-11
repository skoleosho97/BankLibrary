using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Middleware.Data;
using Testcontainers.PostgreSql;
using Tests.Utils;
using Xunit;

namespace Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer container = new PostgreSqlBuilder().Build();

        public async Task InitializeAsync()
        {
            await container.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await container.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveDbContext<AppDbContext>();
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(container.GetConnectionString());
                });
                services.InitDbContext<AppDbContext>();
            });
        }
    }
}
