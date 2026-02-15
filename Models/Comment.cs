using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jira_clone_backend.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }


        [Required]
        public int TaskId { get; set; }

        [Required]
        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
