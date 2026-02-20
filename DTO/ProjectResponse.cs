using System.ComponentModel.DataAnnotations;

namespace jira_clone_backend.DTO
{
    public class ProjectResponse
    {

        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
