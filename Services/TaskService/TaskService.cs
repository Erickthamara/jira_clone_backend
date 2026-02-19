using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.TaskService
{
    public class TaskService : ITaskService
    {
        public Task<TaskResponse> AddTaskAsync(TaskResponse NewTask)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskResponse>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse> GetSingleTaskByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTaskAsync(TaskResponse TaskObject)
        {
            throw new NotImplementedException();
        }
    }
}
