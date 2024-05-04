using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Repositories.Projects;

namespace ProjectManagement.IntegrationalTest;

public class ProjectsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProjectsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IProjectsRepository, ProjectsRepository>();
                services.AddScoped<IProjectsService, ProjectsService>();
            });
        });
    }

    [Fact]
    public async Task GetAll_ReturnsAllProjects()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/projects");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var projects = JsonSerializer.Deserialize<ProjectDto[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        projects.Should().NotBeNull();
        projects.Should().HaveCount(3); 
    }
}