using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectPulse.Web.Services.Tasks;

public interface ITasksService
{
    public Task<List<TaskDto>?> GetTasks(string projectId);


    public Task CreateTask(CreateTaskDto newTask, string projectId);

    public Task DeleteTask(string taskId);
}