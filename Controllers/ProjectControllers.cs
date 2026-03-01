using jira_clone_backend.DTO;
using jira_clone_backend.Services.ProjectService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jira_clone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsControllers : ControllerBase
    {

        private readonly IProjectService projectService;

        public ProjectsControllers(IProjectService service) => projectService = service;

        [HttpGet]
        public async Task<ActionResult<ProjectResponse>> GetProjects()
        {
            return Ok(await projectService.GetAllProjectsAsync());
        }

        [HttpGet]
        public async Task<ActionResult<ProjectResponse>> GetSingleProject(int Id)
        {
            var response = await projectService.GetSingleProjectByIdAsync(Id);

            if (response == null) { return NotFound("Project with the given Id was not found."); }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> AddProject(ProjectResponse newProject)
        {
            var response = await projectService.AddProjectAsync(newProject);

            if (response == null) { return NotFound("Project with the given Id was not found."); }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int Id, ProjectResponse project)
        {
            var IsUpdated = await projectService.UpdateProjectAsync(Id, project);

            if (IsUpdated == false) { return NotFound("Project with the given Id was not found."); }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            var IsProjectDeleted = await projectService.DeleteProjectAsync(Id);
            if (IsProjectDeleted == false)
            {
                return NotFound("Project with the given ID was not found");
            }

            return NoContent();

        }
    }
}
