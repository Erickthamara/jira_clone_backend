using jira_clone_backend.DTO;
using jira_clone_backend.Services.TaskService;
using Microsoft.AspNetCore.Mvc;

namespace jira_clone_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService taskservice;

        public TaskController(ITaskService service) => taskservice = service;

        [HttpGet]
        public async Task<ActionResult<TaskResponse>> GetTasks()
        {
            return Ok(await taskservice.GetAllTasksAsync());
        }

        [HttpGet]
        public async Task<ActionResult<TaskResponse>> GetSingleTask(int Id)
        {
            var response = await taskservice.GetSingleTaskByIdAsync(Id);

            if (response == null) { return NotFound("Task with the given Id was not found."); }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> AddTask(TaskResponse newTask)
        {
            var response = await taskservice.AddTaskAsync(newTask);

            if (response == null) { return NotFound("Task with the given Id was not found."); }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int Id, TaskResponse task)
        {
            var IsUpdated = await taskservice.UpdateTaskAsync(Id, task);

            if (IsUpdated == false) { return NotFound("Task with the given Id was not found."); }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTask(int Id)
        {
            var IsTaskDeleted = await taskservice.DeleteTaskAsync(Id);
            if (IsTaskDeleted == false)
            {
                return NotFound("Task with the given ID was not found");
            }

            return NoContent();

        }

    }
}

