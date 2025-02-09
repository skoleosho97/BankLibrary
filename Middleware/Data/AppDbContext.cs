using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default timestamps in Applicant
            modelBuilder.Entity<Applicant>().Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Applicant>().Property(b => b.LastModified).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Many-to-many relationship between Applicant and Application
            modelBuilder.Entity<Applicant>()
                .HasMany(e => e.Applications)
                .WithMany(e => e.Applicants)
                .UsingEntity("Application_Applicant");

            // Many-to-one relationship between Application and Applicant
            modelBuilder.Entity<Applicant>()
                .HasMany(e => e.IApplications)
                .WithOne(e => e.PrimaryApplicant)
                .HasForeignKey(e => e.PrimaryApplicantId)
                .IsRequired();
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
    }

}
