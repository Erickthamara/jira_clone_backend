using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jira_clone_backend.Models
{
    public class Label
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;
    }
}
