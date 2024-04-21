using ProjectManagementAPI.DTOs.Projects;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Projects;

public interface IProjectRepository
{
    public Task<IEnumerable<Project>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectFromRequestDto projectFromRequestDto);

    public Task<ProjectEntity?> Update(Guid id, ProjectFromRequestDto projectFromRequestDto);

    public Task<int> Delete(Guid id);
}