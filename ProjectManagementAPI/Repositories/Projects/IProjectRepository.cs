using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Projects;

public interface IProjectRepository
{
    public Task<IEnumerable<Project>> GetAll();

    public Task<Project?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectEntity projectEntity);

    public Task<ProjectEntity?> Update(Guid id, ProjectEntity projectEntity);

    public Task<int> Delete(Guid id);
}