using Xunit;

namespace Tests.Fixtures
{
    [CollectionDefinition("Shared")]
    public class SharedFixture : ICollectionFixture<CustomWebApplicationFactory>
    {
        
    }
}
