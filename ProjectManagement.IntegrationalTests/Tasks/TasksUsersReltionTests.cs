// using System.Net.Http.Json;
// using System.Text;
// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Newtonsoft.Json;
// using ProjectManagement.DataAccess.DTOs.Tasks;
//
// namespace ProjectManagement.IntegrationalTests.Tasks;
//
// [Collection("ControllerTestCollection")]
// public class TasksUsersRelationTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
// {
//     private readonly WebApplicationFactory<Program> _factory = factory;
//
//     [Fact]
//     public async Task AssingUser_WithExisingIdsAndStatusToDo_AssignsUser()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         
//         // Act
//         var response = await client.PutAsync("api/tasks/9f4a9b16-45b1-4895-a3c6-8e2eb8deae58/assign_user?userId=fcd21c1e-914c-4a6f-aa18-41505d29c8e7", null);
//         response.EnsureSuccessStatusCode();
//
//         var allTasksResponse = await client.GetAsync("api/tasks");
//         allTasksResponse.EnsureSuccessStatusCode();
//         var tasks = await allTasksResponse.Content.ReadFromJsonAsync<List<TaskDto>>();
//
//         var updatedTask = tasks!.Find(i => i.Id == new Guid("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58"));
//
//         // Assert
//         tasks.Should().NotBeNull();
//         updatedTask!.AssignedUserId.Should().Be(new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7"));
//     }
// }