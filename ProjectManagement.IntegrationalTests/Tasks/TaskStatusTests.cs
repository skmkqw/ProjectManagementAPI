using Microsoft.AspNetCore.Mvc.Testing;

namespace ProjectManagement.IntegrationalTests.Tasks;

[Collection("ControllerTestCollection")]
public class TaskStatusTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;
}