using jira_clone_backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace jira_clone_backend.DTO
{
    public class TaskResponse
    {

        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }

        public int ParentTaskId { get; set; }

        [Required]
        public int ReporterId { get; set; }


        [Required]
        public int AssigneeId { get; set; }


        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TaskStatusEnum? Status { get; set; }

        public TaskPriority? Priority { get; set; }

        public string Type { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }
    }
}
