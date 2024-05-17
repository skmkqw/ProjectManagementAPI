using ProjectManagement.Core.Entities;
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

    #region GET METHODS

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        var projectEntities = await _projectsRepository.GetAll();

        return projectEntities.Select(p => p.ToProjectModel());
    }

    public async Task<Project?> GetProjectById(Guid id)
    {
        var projectEntity = await _projectsRepository.GetById(id);

        return projectEntity?.ToProjectModel();
    }
    
    public async Task<IEnumerable<ProjectTask>?> GetProjectTasks(Guid projectId)
    {
        var taskEntities = await _projectsRepository.GetTasks(projectId);
        return taskEntities?.Select(t => t.ToTaskModel());
    }

    public async Task<IEnumerable<ProjectTask>?> GetUserTasks(Guid userId, Guid projectId)
    {
        var taskEntities = await _projectsRepository.GetUserTasks(projectId, userId);
        return taskEntities?.Select(t => t.ToTaskModel());
    }

    public async Task<IEnumerable<AppUser>?> GetProjectUsers(Guid projectId)
    {
        var userEntities = await _projectsRepository.GetUsers(projectId);
        return userEntities;
    }

    #endregion GET METHODS


    #region POST METHODS

    public async Task<Project> CreateProject(CreateProjectDto createProjectDto)
    {
        var projectEntity = createProjectDto.FromCreateDtoToProjectEntity();

        var createdEntity = await _projectsRepository.Create(projectEntity);

        return createdEntity.ToProjectModel();
    }

    public async Task<ProjectTask?> AddTask(Guid projectId, CreateTaskDto createTaskDto)
    {
        var taskEntity = createTaskDto.FromCreateDtoToTaskEntity();
        var createdEntity = await _projectsRepository.AddTask(projectId, taskEntity);
        return createdEntity?.ToTaskModel();
    }

    #endregion POST METHODS


    #region PUT METHODS
    public async Task<Project?> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var projectEntity = await _projectsRepository.GetById(id);

        if (projectEntity == null) return null;

        await _projectsRepository.Update(projectEntity, updateProjectDto);

        return projectEntity.ToProjectModel();
    }

    public async Task<(ProjectUserEntity? projectUserEntity, string? error)> AddUserToProject(Guid projectId, Guid userId)
    {
        return await _projectsRepository.AddUser(projectId, userId);
    }
    
    public async Task<(Guid? userId, string? error)> RemoveUserFromProject(Guid projectId, Guid userId)
    {
        return await _projectsRepository.RemoveUser(projectId, userId);
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task<bool> DeleteProject(Guid id)
    {
        bool isDeleted = await _projectsRepository.Delete(id);
        return isDeleted;
    }

    #endregion DELETE METHODS
}