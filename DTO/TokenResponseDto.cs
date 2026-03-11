namespace jira_clone_backend.DTO
{
    public class TokenResponseDto
    {
        public required string JWTToken { get; set; }
        public required string RefreshToken { get; set; }
        public required int UserId { get; set; }
        public required string Email { get; set; }
    }
}
