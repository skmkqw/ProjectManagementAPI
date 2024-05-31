using System.Text;
using Newtonsoft.Json;
using ProjectPulse.DataAccess.DTOs.Projects;

namespace ProjectPulse.Web.Services.Projects;

public class ProjectsService : IProjectsService
{

    private readonly HttpClient _httpClient;

    public ProjectsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<ProjectDto>?> GetProjects(string userId)
    {
        var response = await _httpClient.GetAsync($"api/users/{userId}/projects");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
        }
        throw new ApplicationException("Unable to fetch user's projects");
    }

    public async Task<ProjectDto?> GetProject(Guid projectId)
    {
        var response = await _httpClient.GetAsync($"api/projects/{projectId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProjectDto>();
        }
        throw new ApplicationException("Unable to fetch user's projects");
    }

    public async Task CreateProject(CreateProjectDto newProject, string userId)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(newProject), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"api/projects?creatorId={userId}", httpContent);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new ApplicationException("Failed to create a project");
        }
    }
}