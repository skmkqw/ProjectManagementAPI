using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.IntegrationalTests.Helpers;

namespace ProjectManagement.IntegrationalTests.Projects;

[Collection("ControllerTestCollection")]
public class ProjectTaskRelationTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;
    
    [Fact]
    public async Task GetTasks_WithExistingProjectId_ReturnsTasks()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/tasks");
        response.EnsureSuccessStatusCode();
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskDto>>();

        // Assert
        tasks.Should().NotBeNull();
        tasks.Should().HaveCount(1);
        tasks![0].Id.Should().Be("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58");
    }
    
    [Fact]
    public async Task GetTasks_WithNotExistingProjectId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a/tasks");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateTask_WithExistingProjectId_CreatesTask()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var createTaskDto = new CreateTaskDto()
        {
            Title = "Task 5",
            Description = "Description for task 5"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(createTaskDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PostAsync($"api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/add_task", httpContent);
        response.EnsureSuccessStatusCode();
        
        var allTasksResponse = await client.GetAsync("api/tasks");
        allTasksResponse.EnsureSuccessStatusCode();
        var tasks = await allTasksResponse.Content.ReadFromJsonAsync<List<TaskDto>>();

        var newTask = tasks!.Find(pi => pi.ProjectId == new Guid("d99b037b-1e3a-4de0-812f-90e35b30f07a") && pi.Id != new Guid("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58"));
        
        // Assert
        tasks.Should().NotBeNull();
        tasks.Should().HaveCount(4);

        newTask.Should().NotBeNull();
        newTask!.Title.Should().Be("Task 5");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task CreateTask_WithNotExistingProjectId_CreatesTask()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var createTaskDto = new CreateTaskDto()
        {
            Title = "Task 5",
            Description = "Description for task 5"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(createTaskDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PostAsync($"api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a/add_task", httpContent);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}