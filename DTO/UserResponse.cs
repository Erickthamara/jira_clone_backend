using System.ComponentModel.DataAnnotations;

namespace jira_clone_backend.DTO
{
    public class UserResponse
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;


        public string? AvatarUrl { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
