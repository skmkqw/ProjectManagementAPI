using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories;

public interface IProjectRepository
{
    public IEnumerable<Project> GetAll();

    public Project? GetById(int id);

    public Project Create(ProjectFromRequestDto projectFromRequestDto);

    public Project? Update(int id, ProjectFromRequestDto projectFromRequestDto);

    public Project? Delete(int id);
}