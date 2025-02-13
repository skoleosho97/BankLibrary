using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Tests.EndpointTests
{
    [Collection("Shared")]
    public class ApplicationEndpointTests
    {
        private readonly HttpClient client;
        private readonly ITestOutputHelper output;

        public ApplicationEndpointTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {
            client = factory.CreateClient();
            this.output = output;
        }

        [Fact]
        public async Task GET_POST_ReturnCreated_Apply()
        {
            CreateApplicationRequest request = new()
            {
                ApplicationType = "CHECKING",
                NoNewApplicants = true,
                ApplicantIds = [1]
            };

            var apply_response = await client.PostAsJsonAsync("/applications", request);
            apply_response.StatusCode.Should().Be(HttpStatusCode.Created);

            var application = await apply_response.Content.ReadFromJsonAsync<ApplyResponse>();

            if (application is not null)
            {
                output.WriteLine("Id: {0}", application.Id.ToString());
                output.WriteLine("Type: {0}", application.ApplicationType);
                output.WriteLine("Applicants: {0}", application.Applicants);
                output.WriteLine("Status: {0}", application.ApplicationStatus);
                output.WriteLine("Reasons: {0}", application.Reasons);
                output.WriteLine("Accounts Created?: {0}", application.AccountsCreated);
                output.WriteLine("Created Accounts: {0}", application.CreatedAccounts);
                output.WriteLine("Members Created?: {0}", application.MembersCreated);
                output.WriteLine("Created Members: {0}", application.CreatedMembers);
            }
        }
    }
}
