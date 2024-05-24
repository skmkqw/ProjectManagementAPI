using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using ProjectPulse.DataAccess.DTOs.Tasks;
using ProjectPulse.IntegrationalTests.Helpers;

namespace ProjectPulse.IntegrationalTests.Tasks;

[Collection("ControllerTestCollection")]
public class TasksUsersRelationTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task AssingUser_WithAllCompletedConditions_AssignsUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58/assign_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7", null);
        response.EnsureSuccessStatusCode();

        var allTasksResponse = await client.GetAsync("api/tasks");
        allTasksResponse.EnsureSuccessStatusCode();
        var tasks = await allTasksResponse.Content.ReadFromJsonAsync<List<TaskDto>>();

        var updatedTask = tasks!.Find(i => i.Id == new Guid("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58"));

        // Assert
        tasks.Should().NotBeNull();
        updatedTask!.AssignedUserId.Should().Be(new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7"));
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        Utilities.Cleanup(db, userManager);
    }

    [Fact]
    public async Task AssignUser_WithUserNotInProject_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58/assign_user?userId=b1b2a921-af2a-4e38-a6e9-3d38e540dca9", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Can't assign task to user until user is added to project");
    }
    
    [Fact]
    public async Task AssignUser_WithTaskStatusEqual2_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PutAsync("api/tasks/2e9f7e98-4870-4c04-9e9b-5946f2a30ae9/assign_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Can't reassign user for completed task!");
    }
    
    [Fact]
    public async Task AssignUser_WithNotExisingTaskId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae59/assign_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Task not found");
    }
    
    [Fact]
    public async Task AssignUser_WithNotExistingUserId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58/assign_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e0", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("User not found");
    }
    
    [Fact]
    public async Task RemoveUser_WithAllValidConditions_RemovesUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/tasks/6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a08a/remove_user");
        response.EnsureSuccessStatusCode();
        
        var userTasksResponse = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7/tasks");
        userTasksResponse.EnsureSuccessStatusCode();
        var userTasks = await userTasksResponse.Content.ReadFromJsonAsync<List<TaskDto>>();

        var updatedTaskResponse = await client.GetAsync("api/tasks/6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a08a");
        updatedTaskResponse.EnsureSuccessStatusCode();
        var task = await updatedTaskResponse.Content.ReadFromJsonAsync<TaskDto>();
        
        // Assert
        userTasks.Should().BeEmpty();
        task!.AssignedUserId.Should().BeNull();
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        Utilities.Cleanup(db, userManager);
    }
    
    [Fact]
    public async Task RemoveUser_WithNotExisingTaskId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae50/remove_user");
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Task not found");
    }
    
    [Fact]
    public async Task RemoveUser_WithTaskStatusEquals2_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/tasks/2e9f7e98-4870-4c04-9e9b-5946f2a30ae9/remove_user");
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Can't remove assigned user from completed task!");
    }
}