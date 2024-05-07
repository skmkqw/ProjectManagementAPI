using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectManagement.DataAccess.Data;
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
        var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();

        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(3);

    }
    
    [Fact]
    public async Task GetById_WithExistingId_ReturnsUsers()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<UserDto>();


        // Assert
        user.Should().NotBeNull();
        user!.Id.Should().Be("fcd21c1e-914c-4a6f-aa18-41505d29c8e7");
        user.FirstName.Should().Be("Timofei");
    }
    
    [Fact]
    public async Task GetById_WithNotExistingId_ReturnsUsers()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e6");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateUser_CreatesUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var createUserDto = new CreateUserDto()
        {
            FirstName = "Andrew",
            LastName = "Jackson",
            Email = "ajackson@gmail.com",
            Login = "ajackson",
            Password = "jack1241"
        };
        
        var httpContent = new StringContent(JsonConvert.SerializeObject(createUserDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PostAsync("api/users", httpContent);
        response.EnsureSuccessStatusCode();
        var createdUser = await response.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        createdUser.Should().NotBeNull();
        createdUser!.FirstName.Should().Be("Andrew");
        createdUser.Login.Should().Be("ajackson");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task UpdateUser_WithExistingId_UpdatesUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateUserDto = new CreateUserDto()
        {
            FirstName = "Andrew",
            LastName = "Jackson",
            Email = "ajackson@gmail.com",
            Login = "ajackson",
            Password = "jack1241"
        };
        
        var httpContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PutAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e7", httpContent);
        response.EnsureSuccessStatusCode();
        var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        updatedUser.Should().NotBeNull();
        updatedUser!.FirstName.Should().Be("Andrew");
        updatedUser.Login.Should().Be("ajackson");
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task UpdateUser_WithNotExistingId_UpdatesUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var updateUserDto = new CreateUserDto()
        {
            FirstName = "Andrew",
            LastName = "Jackson",
            Email = "ajackson@gmail.com",
            Login = "ajackson",
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
        var users = await allUsersResponse.Content.ReadFromJsonAsync<List<UserDto>>();
        
        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(2);
        
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.Cleanup(db);
    }
    
    [Fact]
    public async Task DeleteUser_WithNotExisingId_DeletesUser()
    {
        // Arrange 
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("api/users/fcd21c1e-914c-4a6f-aa18-41505d29c8e6");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}