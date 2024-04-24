using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Mappers;
using ProjectManagement.DataAccess.Repositories.Projects;

namespace ProjectManagement.Application.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository _projectsRepository;

    public ProjectsService(IProjectsRepository projectsProjectsRepository)
    {
        _projectsRepository = projectsProjectsRepository;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        var projectEntities = await _projectsRepository.GetAll();
        
        List<Project> projects = new();
        foreach (var projectEntity in projectEntities)
        {
            projects.Add(projectEntity.ToProjectModel());
        }

        return projects;
    }

    public async Task<Project> GetProjectById(Guid id)
    {
        var projectEntity = await _projectsRepository.GetById(id);
        
        if (projectEntity == null)
        {
            return null;
        }

        return projectEntity.ToProjectModel();
    }

    public async Task<Project> CreateProject(ProjectFromRequestDto projectFromRequestDto)
    {
        var projectEntity = projectFromRequestDto.FromDtoToProjectEntity();

        var createdEntity = await _projectsRepository.Create(projectEntity);

        return createdEntity.ToProjectModel();
    }

    public async Task<ProjectTask> AddTask(Guid projectId, CreateTaskDto createTaskDto)
    {
        try
        {
            var taskEntity = createTaskDto.FromDtoToTaskEntity();
            var createdEntity = await _projectsRepository.AddTask(projectId, taskEntity);
            return createdEntity.ToTaskModel();
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }

    public async Task<Project> UpdateProject(Guid id, ProjectFromRequestDto projectFromRequestDto)
    {
        var projectEntity = await _projectsRepository.GetById(id);
        
        if (projectEntity == null)
            throw new ArgumentException("Project not found");

        projectEntity.Name = projectFromRequestDto.Name;
        projectEntity.Description = projectFromRequestDto.Description;

        await _projectsRepository.Update(projectEntity);

        return projectEntity.ToProjectModel();
    }

    public async Task DeleteProject(Guid id)
    {
        try
        {
            await _projectsRepository.Delete(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}