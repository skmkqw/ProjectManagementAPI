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

    public async Task<Project> GetProjectById(Guid id)
    {
        var projectEntity = await _projectsRepository.GetById(id);
        
        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }

        return projectEntity.ToProjectModel();
    }
    
    public async Task<IEnumerable<ProjectTask>> GetProjectTasks(Guid projectId)
    {
        try
        {
            var taskEntities = await _projectsRepository.GetTasks(projectId);
            return taskEntities.Select(t => t.ToTaskModel());
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }
    
    public async Task<IEnumerable<User>> GetProjectUsers(Guid projectId)
    {
        try
        {
            var userEntities = await _projectsRepository.GetUsers(projectId);
            return userEntities.Select(t => t.ToUserModel());
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    #endregion GET METHODS


    #region POST METHODS

    public async Task<Project> CreateProject(CreateProjectDto createProjectDto)
    {
        var projectEntity = createProjectDto.FromCreateDtoToProjectEntity();

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

    #endregion POST METHODS


    #region PUT METHODS
    public async Task<Project> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var projectEntity = await _projectsRepository.GetById(id);

        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }

        await _projectsRepository.Update(projectEntity, updateProjectDto);

        return projectEntity.ToProjectModel();
    }

    public async Task<ProjectUserEntity> AddUserToProject(Guid projectId, Guid userId)
    {
        try
        {
            var projectUserEntity = await _projectsRepository.AddUser(projectId, userId);
            return projectUserEntity;
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    public async Task RemoveUserFromProject(Guid projectId, Guid userId)
    {
        try
        {
            await _projectsRepository.RemoveUser(projectId, userId);
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    #endregion PUT METHODS


    #region DELETE METHODS

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

    #endregion DELETE METHODS
}