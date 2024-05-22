using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.IntegrationalTests.Helpers;

namespace ProjectManagement.IntegrationalTests.Tasks;

[Collection("ControllerTestCollection")]
public class TaskStatusTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task UpdateStatus_WithExistingAssignedUserAndTaskId_UpdatesStatus()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PatchAsync("api/tasks/6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a08a/status?status=1", null);
        response.EnsureSuccessStatusCode();

        var updatedTaskResponse = await client.GetAsync("api/Tasks/6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a08a");
        updatedTaskResponse.EnsureSuccessStatusCode();
        var updatedTask = await updatedTaskResponse.Content.ReadFromJsonAsync<TaskDto>();
        
        // Assert
        updatedTask.Should().NotBeNull();
        updatedTask!.Status.Should().Be(TaskStatuses.InProgress);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        Utilities.Cleanup(db, userManager);
    }
    
    [Fact]
    public async Task UpdateStatus_WithNotExistingAssignedUser_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PatchAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58/status?status=1", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Can't change task status before assigning user!");
    }
    
    [Fact]
    public async Task UpdateStatus_WithNotExistingTaskId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PatchAsync("api/tasks/6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a05a/status?status=1", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Task not found");
    }
    
}