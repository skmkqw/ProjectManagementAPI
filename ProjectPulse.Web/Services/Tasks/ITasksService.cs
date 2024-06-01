using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectPulse.Web.Services.Tasks;

public interface ITasksService
{
    public Task<List<TaskDto>?> GetTasks(string projectId);


    public Task CreateTask(CreateTaskDto newTask, string projectId);

    public Task AssignUser(string taskId, string userId);

    public Task ChangeStatus(string taskId, TaskStatuses status);

    public Task DeleteTask(string taskId);
}