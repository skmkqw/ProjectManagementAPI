using System.Text;
using Newtonsoft.Json;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectPulse.Web.Services.Tasks;

public class TasksService : ITasksService
{
    private readonly HttpClient _httpClient;

    public TasksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<TaskDto>?> GetTasks(string projectId)
    {
        var response = await _httpClient.GetAsync($"api/projects/{projectId}/tasks");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TaskDto>>();
        }
        throw new ApplicationException("Unable to fetch tasks");
    }

    public async Task CreateTask(CreateTaskDto newTask, string projectId)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"api/projects/{projectId}/add_task", httpContent);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new ApplicationException("Failed to create a task");
        }
    }

    public async Task AssignUser(string taskId, string userId)
    {
        var response = await _httpClient.PutAsync($"api/tasks/{taskId}/assign_user?userId={userId}", null);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new ApplicationException("Failed to assign user to a task");
        }
    }

    public async Task ChangeStatus(string taskId, TaskStatuses status)
    {
        var response = await _httpClient.PatchAsync($"api/tasks/{taskId}/status?status={status}", null);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new ApplicationException("Failed to change task status");
        }
    }

    public async Task DeleteTask(string taskId)
    {       
        var response = await _httpClient.DeleteAsync($"api/tasks/{taskId}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new ApplicationException("Failed to delete a task");
        }
    }
}