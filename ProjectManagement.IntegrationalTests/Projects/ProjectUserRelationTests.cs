using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.IntegrationalTests.Helpers;

namespace ProjectManagement.IntegrationalTests.Projects;

[Collection("ControllerTestCollection")]
public class ProjectUserRealtionTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;
    
    [Fact]
    public async Task GetUsers_WithExistingProjectId_ReturnsUsers()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/users");
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();


        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(1);
        users![0].Id.Should().Be("fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
    }
    
    [Fact]
    public async Task GetUsers_WithNotExistingProjectId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a/users");
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Project not found");
    }
    
    [Fact]
    public async Task AddUserToProject_WithExistingIds_AddsUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsync("api/projects/5ac6bace-5059-4e5d-bfc1-643d0e9c05cf/add_user?userId=b1b2a921-af2a-4e38-a6e9-3d38e540dca9", null);
        response.EnsureSuccessStatusCode();
        var projectUserEntity = await response.Content.ReadFromJsonAsync<ProjectUsersDto>();

        
        // Assert
        projectUserEntity.Should().NotBeNull();
        projectUserEntity!.ProjectId.Should().Be(new Guid("5ac6bace-5059-4e5d-bfc1-643d0e9c05cf"));
        projectUserEntity.UserId.Should().Be(new Guid("b1b2a921-af2a-4e38-a6e9-3d38e540dca9"));
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task AddUserToProject_WithNotExisingProjectId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsync("api/projects/5ac6bace-5059-4e5d-bfc1-643d0e9c55cf/add_user?userId=b1b2a921-af2a-4e38-a6e9-3d38e540dca9", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Project not found");
    }
    
    [Fact]
    public async Task AddUserToProject_WithNotExisingUserId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsync("api/projects/5ac6bace-5059-4e5d-bfc1-643d0e9c05cf/add_user?userId=b1b2a921-af2a-4e38-a6e9-3d38e540dca0", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("User not found");
    }
    
    [Fact]
    public async Task AddUserToProject_WithExistingRelation_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/add_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7", null);
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("User already exists in the project!");
    }
    
    [Fact]
    public async Task RemoveUserFromProject_WithExistingIds_RemovesUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/remove_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        response.EnsureSuccessStatusCode();

        var projectUsersRepsponse = await client.GetAsync($"api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/users");
        projectUsersRepsponse.EnsureSuccessStatusCode();
        var projectUsers = await projectUsersRepsponse.Content.ReadFromJsonAsync<List<UserDto>>();
        
        // Assert
        projectUsers.Should().BeEmpty();
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task RemoveUserFromProject_WithNotExisingProjectId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f97a/remove_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("Project not found");
    }
    
    [Fact]
    public async Task RemoveUserFromProject_WithNotExisingUserId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/d99b037b-1e3a-4de0-812f-90e35b30f07a/remove_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e0");
        var error = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("User not found");
    }
    
    [Fact]
    public async Task RemoveUserFromProject_WithNotExistingRealtion_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/projects/5ac6bace-5059-4e5d-bfc1-643d0e9c05cf/remove_user?userId=b1b2a921-af2a-4e38-a6e9-3d38e540dca9");
        var error = await response.Content.ReadAsStringAsync();
            
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("There is no such user in the project!");
    }
}