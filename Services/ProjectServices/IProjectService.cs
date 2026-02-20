using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.ProjectService
{
    public interface IProjectService
    {
        Task<List<ProjectResponse?>> GetAllProjectsAsync();

        Task<ProjectResponse?> GetSingleProjectByIdAsync(int Id);

        Task<ProjectResponse> AddProjectAsync(ProjectResponse NewProject);

        Task<bool> UpdateProjectAsync(int Id, ProjectResponse ProjectObject);

        Task<bool> DeleteProjectAsync(int id);
    }
}
