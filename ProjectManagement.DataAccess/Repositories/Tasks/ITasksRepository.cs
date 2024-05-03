using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.DataAccess.Repositories.Tasks;

public interface ITasksRepository
{
    public Task<IEnumerable<ProjectTaskEntity>> GetAll();

    public Task<ProjectTaskEntity?> GetById(Guid id);
    
    public Task<(ProjectTaskEntity? taskEntity, string? error)> AssignUser(Guid taskId, Guid userId);

    public Task<string?> RemoveUser(Guid taskId);
    
    public Task<ProjectTaskEntity> Update(ProjectTaskEntity projectTaskEntity, UpdateTaskDto updateTaskDto);

    public Task<ProjectTaskEntity> UpdateStatus(ProjectTaskEntity projectTaskEntity);

    public Task<bool> Delete(Guid id);
}