using jira_clone_backend.Data;
using jira_clone_backend.DTO;
using jira_clone_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jira_clone_backend.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private JiraContext _dbContext;

        public ProjectService(JiraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectResponse> AddProjectAsync(ProjectResponse NewProject)
        {
            if (NewProject == null) throw new ArgumentNullException(nameof(NewProject));

            var NewProjectResponse = new Project
            {
                Name = NewProject.Name,
                UserId = NewProject.UserId,
                Description = NewProject.Description
            };
            if (NewProjectResponse != null)
                await _dbContext.Projects.AddAsync(NewProjectResponse);

            await _dbContext.SaveChangesAsync();

            return new ProjectResponse
            {
                UserId = NewProjectResponse.UserId,
                Description = NewProjectResponse.Description,
                Name = NewProjectResponse.Name
            };
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            Project ProjectToDelete = await _dbContext.Projects.FindAsync(id);

            if (ProjectToDelete == null)
            {
                return false;
            }

            _dbContext.Projects.Remove(ProjectToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectResponse>> GetAllProjectsAsync()
        {
            List<ProjectResponse> projectResponses = [];
            projectResponses = await _dbContext.Projects.Select(p => new ProjectResponse
            {
                UserId = p.UserId,
                Description = p.Description,
                Name = p.Name
            }).ToListAsync();

            return projectResponses;

        }

        public async Task<ProjectResponse?> GetSingleProjectByIdAsync(int Id)
        {
            Project project = await _dbContext.Projects.FindAsync(Id);

            if (project == null) return new ProjectResponse { };

            return new ProjectResponse
            {
                UserId = project.UserId,
                Description = project.Description,
                Name = project.Name
            };
        }

        public async Task<bool> UpdateProjectAsync(int Id, ProjectResponse ProjectObject)
        {
            Project project = await _dbContext.Projects.FindAsync(Id);

            if (project == null) return false;

            project.Description = ProjectObject.Description;
            project.Name = ProjectObject.Name;
            project.UserId = ProjectObject.UserId;

            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}
