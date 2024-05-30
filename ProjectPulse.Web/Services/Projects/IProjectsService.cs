using ProjectPulse.DataAccess.DTOs.Projects;

namespace ProjectPulse.Web.Services.Projects;

public interface IProjectsService
{
    public Task<List<ProjectDto>?> GetProjects(string userId);

    public Task CreateProject(CreateProjectDto newProject, string userId);
}