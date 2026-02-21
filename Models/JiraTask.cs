using jira_clone_backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jira_clone_backend.Models
{
    public class JiraTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int ParentTaskId { get; set; }

        [Required]
        public int ReporterId { get; set; }


        [Required]
        public int AssigneeId { get; set; }
        public User? User { get; set; }


        [Required]
        public string Title { get; set; } = string.Empty;

        public TaskStatusEnum? Status { get; set; }

        public TaskPriority? Priority { get; set; }

        public string Type { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }




}


