using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories;

public interface IProjectRepository : IGenericRepository<Project>
{ 
    public Task<Project?> Create(ProjectFromRequestDto projectFromRequestDto);

    public Task<Project?> Update(int id, ProjectFromRequestDto projectFromRequestDto);
}