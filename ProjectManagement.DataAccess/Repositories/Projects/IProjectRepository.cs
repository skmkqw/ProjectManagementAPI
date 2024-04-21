using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public interface IProjectRepository
{
    public Task<IEnumerable<Project>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectFromRequestDto projectFromRequestDto);

    public Task<ProjectEntity?> Update(Guid id, ProjectFromRequestDto projectFromRequestDto);

    public Task<int> Delete(Guid id);
}