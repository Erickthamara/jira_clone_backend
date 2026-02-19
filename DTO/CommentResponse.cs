using System.ComponentModel.DataAnnotations;

namespace jira_clone_backend.DTO
{
    public class CommentResponse
    {
        [Required]
        public int UserId { get; set; }


        [Required]
        public int TaskId { get; set; }

        [Required]
        public string Body { get; set; } = string.Empty;
    }
}
