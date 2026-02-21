using jira_clone_backend.Data;
using jira_clone_backend.DTO;
using jira_clone_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jira_clone_backend.Services.TaskService
{
    public class TaskService : ITaskService
    {


        private JiraContext _dbContext;

        public TaskService(JiraContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TaskResponse> AddTaskAsync(TaskResponse NewTask)
        {

            if (NewTask == null) throw new ArgumentNullException(nameof(NewTask));

            var NewTaskREsponse = new JiraTask
            {
                Description = NewTask.Description,
                Title = NewTask.Title,
                Id = NewTask.Id,
                ParentTaskId = NewTask.ParentTaskId,
                ReporterId = NewTask.ReporterId,
                AssigneeId = NewTask.AssigneeId,
                Type = NewTask.Type,
                Priority = NewTask.Priority,
                Status = NewTask.Status,
                DueDate = NewTask.DueDate,
                ProjectId = NewTask.ProjectId,
            };

            if (NewTaskREsponse != null)
                await _dbContext.Tasks.AddAsync(NewTaskREsponse);

            await _dbContext.SaveChangesAsync();

            return new TaskResponse
            {
                Description = NewTask.Description,
                Title = NewTask.Title,
                Id = NewTask.Id,
                ParentTaskId = NewTask.ParentTaskId,
                ReporterId = NewTask.ReporterId,
                AssigneeId = NewTask.AssigneeId,
                Type = NewTask.Type,
                Priority = NewTask.Priority,
                Status = NewTask.Status,
                DueDate = NewTask.DueDate,
                ProjectId = NewTask.ProjectId,
            };
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            JiraTask TaskToDelete = await _dbContext.Tasks.FindAsync(id);

            if (TaskToDelete == null)
            {
                return false;
            }

            _dbContext.Tasks.Remove(TaskToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskResponse>> GetAllTasksAsync()
        {
            List<TaskResponse> taskResponses = [];
            taskResponses = await _dbContext.Tasks.Select(p => new TaskResponse
            {
                Description = p.Description,
                ReporterId = p.ReporterId,
                DueDate = p.DueDate,
                Status = p.Status,
                Priority = p.Priority,
                Title = p.Title,
                AssigneeId = p.AssigneeId,
                Id = p.Id,
                ParentTaskId = p.ParentTaskId,
                ProjectId = p.ProjectId,
                Type = p.Type


            }).ToListAsync();

            return taskResponses;
        }

        public async Task<TaskResponse> GetSingleTaskByIdAsync(int Id)
        {
            JiraTask task = await _dbContext.Tasks.FindAsync(Id);

            if (task == null) return new TaskResponse { };

            return new TaskResponse
            {
                Description = task.Description,
                ReporterId = task.ReporterId,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
                Title = task.Title,
                AssigneeId = task.AssigneeId,
                Id = task.Id,
                ParentTaskId = task.ParentTaskId,
                ProjectId = task.ProjectId,
                Type = task.Type
            };
        }

        public async Task<bool> UpdateTaskAsync(int Id, TaskResponse TaskObject)
        {
            JiraTask task = await _dbContext.Tasks.FindAsync(Id);

            if (task == null) return false;

            task.Description = TaskObject.Description;
            task.ReporterId = TaskObject.ReporterId;
            task.DueDate = TaskObject.DueDate;
            task.Status = TaskObject.Status;
            task.Priority = TaskObject.Priority;
            task.Title = TaskObject.Title;
            task.AssigneeId = TaskObject.AssigneeId;
            task.Id = TaskObject.Id;
            task.ProjectId = TaskObject.ProjectId;
            task.Type = TaskObject.Type;


            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
