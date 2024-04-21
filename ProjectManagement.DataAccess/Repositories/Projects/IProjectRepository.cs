using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public interface IProjectRepository
{
    public Task<IEnumerable<ProjectEntity>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectEntity projectEntity);

    public Task<ProjectEntity?> Update(ProjectEntity projectEntity);

    public Task Delete(Guid id);
}