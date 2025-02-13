using Core.Models;
using Core.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Id
            modelBuilder.Entity<Applicant>().Property(b => b.Id).UseIdentityByDefaultColumn();
            modelBuilder.Entity<Application>().Property(b => b.Id).UseIdentityByDefaultColumn();

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

            // Many-to-many relationship between Account and Member
            modelBuilder.Entity<Member>()
                .HasMany(e => e.Accounts)
                .WithMany(e => e.Members)
                .UsingEntity("Account_Holder");

            // Many-to-one relationship between Account and Member
            modelBuilder.Entity<Account>()
                .HasOne(e => e.PrimaryAccountHolder)
                .WithMany()
                .HasForeignKey(e => e.PrimaryAccountHolderId)
                .IsRequired();
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

}
