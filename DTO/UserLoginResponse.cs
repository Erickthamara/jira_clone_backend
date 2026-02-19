using System.ComponentModel.DataAnnotations;

namespace jira_clone_backend.DTO
{
    public class UserLoginResponse
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
