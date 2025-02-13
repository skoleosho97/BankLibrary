using System;
using System.Collections.Generic;
using Bogus;
using Core.Dtos.Requests;
using Core.Models;
using Middleware.Data;

namespace Tests.Utils
{
    public class MockDataGenerator
    {
        private static int ApplicantId = 0;

        public static int IncrementId()
        {
            ApplicantId++;

            return ApplicantId;
        }
        private static readonly Faker<Applicant> ApplicantGenerator = 
            new Faker<Applicant>()
                .RuleFor(x => x.Id, _ => IncrementId())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.MiddleName, f => f.PickRandom(f.Name.FirstName(), ""))
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.DateOfBirth, f => f.Date.RecentDateOnly())
                .RuleFor(x => x.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Phone, f => new Randomizer().Replace("##########"))
                .RuleFor(x => x.SocialSecurity, f => new Randomizer().Replace("########"))
                .RuleFor(x => x.DriversLicense, new Randomizer().Replace("############"))
                .RuleFor(x => x.Income, f => f.Random.Number(1,9)*10000)
                .RuleFor(x => x.Address, f => f.Address.StreetAddress())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.State, f => f.Address.State())
                .RuleFor(x => x.Zipcode, f => f.Address.ZipCode())
                .RuleFor(x => x.MAddress, (f, x) => x.Address)
                .RuleFor(x => x.MCity, (f, x) => x.City)
                .RuleFor(x => x.MState, (f, x) => x.State)
                .RuleFor(x => x.MZipcode, (f, x) => x.Zipcode)
                .RuleFor(x => x.LastModified, _ => DateTime.Now)
                .RuleFor(x => x.CreatedAt, _ => DateTime.Now);

        public static readonly Faker<CreateApplicantRequest> ApplicantRequestGenerator = 
            new Faker<CreateApplicantRequest>()
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.MiddleName, f => f.PickRandom(f.Name.FirstName(), ""))
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.DateOfBirth, f => f.Date.RecentDateOnly())
                .RuleFor(x => x.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Phone, f => new Randomizer().Replace("##########"))
                .RuleFor(x => x.SocialSecurity, f => new Randomizer().Replace("########"))
                .RuleFor(x => x.DriversLicense, new Randomizer().Replace("############"))
                .RuleFor(x => x.Income, f => f.Random.Number(1,9)*10000)
                .RuleFor(x => x.Address, f => f.Address.StreetAddress())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.State, f => f.Address.State())
                .RuleFor(x => x.Zipcode, f => f.Address.ZipCode())
                .RuleFor(x => x.MAddress, (f, x) => x.Address)
                .RuleFor(x => x.MCity, (f, x) => x.City)
                .RuleFor(x => x.MState, (f, x) => x.State)
                .RuleFor(x => x.MZipcode, (f, x) => x.Zipcode);

        public static void Initialize(AppDbContext context)
        {
            List<Applicant> applicants = ApplicantGenerator.Generate(3);

            context.Applicants.AddRange(applicants);
            context.SaveChanges();
        }
    }
}
