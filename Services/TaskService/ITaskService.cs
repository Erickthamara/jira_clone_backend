using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.TaskService
{
    public interface ITaskService
    {
        Task<List<TaskResponse>> GetAllTasksAsync();

        Task<TaskResponse> GetSingleTaskByIdAsync(int Id);

        Task<TaskResponse> AddTaskAsync(TaskResponse NewTask);

        Task<bool> UpdateTaskAsync(TaskResponse TaskObject);

        Task<bool> DeleteTaskAsync(int id);
    }
}
