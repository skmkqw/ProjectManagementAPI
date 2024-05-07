using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.IntegrationalTests.Helpers;

namespace ProjectManagement.IntegrationalTests.Projects;


[Collection("ControllerTestCollection")]
public class ProjectsControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GetAll_ReturnsAllProjects()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/projects");
        response.EnsureSuccessStatusCode();
        var projects = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();

        // Assert
        projects.Should().NotBeNull();
        projects.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetById_WithExistingId_ReturnsProject()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a");
        response.EnsureSuccessStatusCode();
        var project = await response.Content.ReadFromJsonAsync<ProjectDto>();

        // Assert
        project.Should().NotBeNull();
        project!.Id.Should().Be("d99b037b-1e3a-4de0-812f-90e35b30f07a");
        project.Name.Should().Be("Project 1");
        project.Description.Should().Be("Description for Project 1");
    }
    
    [Fact]
    public async Task GetById_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateProject_CreatesProject()
    {
        // Arrange
        var client = _factory.CreateClient();

        var newProject = new CreateProjectDto()
        {
            Name = "Project 4",
            Description = "Description for Project 4"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(newProject), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("api/projects", httpContent);
        response.EnsureSuccessStatusCode();
        var project = await response.Content.ReadFromJsonAsync<ProjectDto>();
        
        var allProjectsResponse = await client.GetAsync("api/projects");
        allProjectsResponse.EnsureSuccessStatusCode();
        var projects = await allProjectsResponse.Content.ReadFromJsonAsync<List<ProjectDto>>();


        // Assert
        project.Should().NotBeNull();
        project!.Name.Should().Be("Project 4");
        projects.Should().HaveCount(4);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }

    [Fact]
    public async Task UpdateProject_WithExistingId_UpdatesProject()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateProjectDto = new UpdateProjectDto()
        {
            Name = "project 4",
            Description = "description for project 4"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateProjectDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PutAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a", httpContent);
        response.EnsureSuccessStatusCode();
        
        var updatedProjectResponse = await client.GetAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a");
        response.EnsureSuccessStatusCode();
        var updatedProject = await updatedProjectResponse.Content.ReadFromJsonAsync<ProjectDto>();

        // Assert
        updatedProject.Should().NotBeNull();
        updateProjectDto.Name.Should().Be("project 4");
        updatedProject!.Description.Should().Be("description for project 4");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }

    [Fact]
    public async Task UpdateProject_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateProjectDto = new UpdateProjectDto()
        {
            Name = "project 4",
            Description = "description for project 4"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateProjectDto), Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PutAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a", httpContent);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteProject_WithExistingId_DeletesProject()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a");
        response.EnsureSuccessStatusCode();
        
        var allProjectsResponse = await client.GetAsync("/api/projects");
        allProjectsResponse.EnsureSuccessStatusCode();
        var projects = await allProjectsResponse.Content.ReadFromJsonAsync<List<ProjectDto>>();
        
        // Assert
        projects.Should().NotBeNull();
        projects.Should().HaveCount(2);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task DeleteProject_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}