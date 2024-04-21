using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Mappers;
using ProjectManagement.DataAccess.Repositories.Projects;

namespace ProjectManagement.Application.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectRepository _repository;

    public ProjectsService(IProjectRepository projectRepository)
    {
        _repository = projectRepository;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        var projectEntities = await _repository.GetAll();
        
        List<Project> projects = new();
        foreach (var projectEntity in projectEntities)
        {
            projects.Add(projectEntity.ToProjectModel());
        }

        return projects;
    }

    public async Task<Project> GetProjectById(Guid id)
    {
        var projectEntity = await _repository.GetById(id);
        
        if (projectEntity == null)
        {
            return null;
        }

        return projectEntity.ToProjectModel();
    }

    public async Task<Project> CreateProject(ProjectFromRequestDto projectFromRequestDto)
    {
        var projectEntity = projectFromRequestDto.FromDtoToProjectEntity();

        var createdEntity = await _repository.Create(projectEntity);

        return createdEntity.ToProjectModel();
    }

    public async Task<Project> UpdateProject(Guid id, ProjectFromRequestDto projectFromRequestDto)
    {
        var projectEntity = await _repository.GetById(id);
        
        if (projectEntity == null)
            throw new ArgumentException("Project not found");

        projectEntity.Name = projectFromRequestDto.Name;
        projectEntity.Description = projectFromRequestDto.Description;

        await _repository.Update(projectEntity);

        return projectEntity.ToProjectModel();
    }

    public async Task DeleteProject(Guid id)
    {
        try
        {
            await _repository.Delete(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}