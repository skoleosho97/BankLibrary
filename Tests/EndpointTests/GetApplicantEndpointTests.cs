using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Tests.EndpointTests
{
    public class GetApplicantEndpointTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient client;

        public GetApplicantEndpointTests(CustomWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_ReturnOk_GetApplicants()
        {
            var response = await client.GetAsync("/applicants");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GET_ReturnNotFound_GetApplicantById()
        {
            int id = 1;

            var response = await client.GetAsync($"/applicants/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
