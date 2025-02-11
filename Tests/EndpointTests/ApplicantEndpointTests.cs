using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Dtos.Responses;
using FluentAssertions;
using Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Tests.EndpointTests
{
    [Collection("Shared")]
    public class ApplicantEndpointTests
    {
        private readonly HttpClient client;
        private readonly ITestOutputHelper output;

        public ApplicantEndpointTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {
            client = factory.CreateClient();
            this.output = output;
        }

        // GET
        [Fact]
        [DisplayTestMethod]
        public async Task GET_ReturnNotNullOrEmpty_GetApplicants()
        {
            var response = await client.GetFromJsonAsync<List<ApplicantResponse>>("applicants");

            response.Should().NotBeNullOrEmpty();

            response.ForEach(applicant =>
            {
                output.WriteLine("Id: {0}", applicant.Id.ToString());
            });
        }
    }
}
