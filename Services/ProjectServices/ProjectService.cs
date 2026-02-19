using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.Project
{
    public class ProjectService : IProjectService
    {
        public Task<ProjectResponse> AddProjectAsync(ProjectResponse NewProject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProjectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectResponse>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectResponse> GetSingleProjectByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProjectAsync(ProjectResponse ProjectObject)
        {
            throw new NotImplementedException();
        }
    }
}
