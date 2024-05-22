using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.IntegrationalTests.Helpers;

namespace ProjectManagement.IntegrationalTests.Users;

[Collection("ControllerTestCollection")]
public class UsersControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GetUsers_ReturnsUsers()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/users");
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<AppUser>>();

        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(3);

    }
    
    [Fact]
    public async Task GetById_WithExistingId_ReturnsUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<AppUser>();


        // Assert
        user.Should().NotBeNull();
        user!.Id.Should().Be("fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
    }
    
    [Fact]
    public async Task GetById_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e6");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    
    [Fact]
    public async Task UpdateUser_WithExistingId_UpdatesUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateUserDto = new AddUserDto()
        {
            Email = "ajackson@gmail.com",
            UserName = "ajackson",
            Password = "jack1241"
        };
        
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PutAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7", httpContent);
        response.EnsureSuccessStatusCode();
        
        var updatedUserResponse = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        response.EnsureSuccessStatusCode();
        var updatedUser = await updatedUserResponse.Content.ReadFromJsonAsync<AppUser>();

        // Assert
        updatedUser.Should().NotBeNull();
        updatedUser!.Email.Should().Be("ajackson@gmail.com");
        updatedUser.UserName.Should().Be("ajackson");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        Utilities.Cleanup(db, userManager);
    }
    
    [Fact]
    public async Task UpdateUser_WithNotExistingId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateUserDto = new AddUserDto()
        {
            Email = "ajackson@gmail.com",
            UserName = "ajackson",
            Password = "jack1241"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PutAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e6", httpContent);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteUser_WithExisingId_DeletesUser()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        response.EnsureSuccessStatusCode();
        
        var allUsersResponse = await client.GetAsync("/api/users");
        allUsersResponse.EnsureSuccessStatusCode();
        var users = await allUsersResponse.Content.ReadFromJsonAsync<List<AppUser>>();
        
        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(2);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        Utilities.Cleanup(db, userManager);
    }
    
    [Fact]
    public async Task DeleteUser_WithNotExisingId_ReturnsNotFound()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e6");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProjects_WithExisingUserId_ReturnsProjects()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7/projects");
        response.EnsureSuccessStatusCode();
        var projects = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();

        // Assert
        projects.Should().NotBeNull();
        projects.Should().HaveCount(1);
        projects![0].Id.Should().Be(new Guid("d99b037b-1e3a-4de0-812f-90e35b30f07a"));
    }
    
    [Fact]
    public async Task GetProjects_WithNotExisingUserId_ReturnsProjects()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e0/projects");
        var error = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be("User not found");
    }
}