using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories;

public interface IProjectRepository
{
    public Task<IEnumerable<Project>> GetAll();

    public Task<Project?> GetById(int id);

    public Task<Project?> Create(ProjectFromRequestDto projectFromRequestDto);

    public Task<Project?> Update(int id, ProjectFromRequestDto projectFromRequestDto);

    public Task<Project?> Delete(int id);
}