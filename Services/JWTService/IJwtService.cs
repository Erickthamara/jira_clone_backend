using jira_clone_backend.DTO;
using jira_clone_backend.Models;

namespace jira_clone_backend.Services.JWTService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Task<string?> SaveAndGenerateRefreshTokenAsync(User user);
        string? GenerateRefreshToken();
        int? GetUserIdFromToken(string token);
        Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<TokenResponseDto> CreateTokens(User user);
    }
}
