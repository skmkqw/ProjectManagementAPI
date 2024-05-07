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

namespace ProjectManagement.IntegrationalTests.Tasks;


[Collection("ControllerTestCollection")]
public class TasksControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GetAll_ReturnsAllTasks()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/tasks");
        response.EnsureSuccessStatusCode();
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskDto>>();

        // Assert
        tasks.Should().NotBeNull();
        tasks.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetById_WithExistingId_ReturnsTask()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58");
        response.EnsureSuccessStatusCode();
        var task = await response.Content.ReadFromJsonAsync<TaskDto>();

        // Assert
        task.Should().NotBeNull();
        task!.Id.Should().Be("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58");
        task.Title.Should().Be("Task 1");
        task.Description.Should().Be("Description for task 1");
    }
    
    [Fact]
    public async Task GetById_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae55");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    

    [Fact]
    public async Task UpdateTask_WithExistingId_UpdatesTask()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateTaskDto = new UpdateTaskDto()
        {
            Title = "Task 4",
            Description = "Description for task 4"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateTaskDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58", httpContent);
        response.EnsureSuccessStatusCode();
        
        var updatedTaskResponse = await client.GetAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58");
        response.EnsureSuccessStatusCode();
        var updatedTask = await updatedTaskResponse.Content.ReadFromJsonAsync<TaskDto>();

        // Assert
        updatedTask.Should().NotBeNull();
        updateTaskDto.Title.Should().Be("Task 4");
        updatedTask!.Description.Should().Be("Description for task 4");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }

    [Fact]
    public async Task UpdateTask_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateTaskDto = new UpdateTaskDto()
        {
            Title = "Task 4",
            Description = "Description for task 4"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateTaskDto), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae55", httpContent);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTask_WithExistingId_DeletesTask()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58");
        response.EnsureSuccessStatusCode();
        
        var allTasksResponse = await client.GetAsync("/api/tasks");
        allTasksResponse.EnsureSuccessStatusCode();
        var tasks = await allTasksResponse.Content.ReadFromJsonAsync<List<TaskDto>>();
        
        // Assert
        tasks.Should().NotBeNull();
        tasks.Should().HaveCount(2);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task DeleteTask_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae55");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}