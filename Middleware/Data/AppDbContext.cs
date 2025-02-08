using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Applicant>().Property(b => b.LastModified).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
    }

}
