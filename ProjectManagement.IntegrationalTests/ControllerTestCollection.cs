namespace ProjectManagement.IntegrationalTests;

[CollectionDefinition("ControllerTestCollection")]
public class CustomIntegrationTestsCollection : ICollectionFixture<TestWebApplicationFactory>
{
}