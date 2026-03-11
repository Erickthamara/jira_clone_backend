using jira_clone_backend.Data;
using jira_clone_backend.DTO;
using jira_clone_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace jira_clone_backend.Services.JWTService
{
    public class JwtService : IJwtService
    {
        private JiraContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly string _jwtKey;

        public JwtService(IConfiguration configuration, JiraContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _jwtIssuer = Environment.GetEnvironmentVariable("JWT_Issuer")!;
            _jwtAudience = Environment.GetEnvironmentVariable("JWT_Audience")!;
            _jwtKey = Environment.GetEnvironmentVariable("JWT_Token")!;
        }

        public string? GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                //new Claim(ClaimTypes.Role,user.Role)
            };
            var token = Environment.GetEnvironmentVariable("JWT_Token") ?? throw new ArgumentNullException("JWT Token missing!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var tokenDescriptor = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_Issuer") ?? throw new ArgumentNullException("JWT Issuer missing!"),
                audience: Environment.GetEnvironmentVariable("JWT_Audience") ?? throw new ArgumentNullException("JWT Audience missing!"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public int? GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false, // We're only extracting userId
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration.GetValue<string>(_jwtKey)!)
                    )
                }, out _);

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;
            try
            {
                var userId = GetUserIdFromToken(refreshToken);
                if (userId == null)
                    return null;


                // Now validate against Supabase
                var user = await ValidateRefreshTokenAsync(userId.Value, refreshToken);
                if (user == null)
                    return null;

                var userModel = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                return await CreateTokens(userModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                //_logger.LogError($"Error in RefreshTokensAsync: {ex.Message}");

                return null;  // Optionally, handle errors here as needed
            }
        }

        public async Task<string?> SaveAndGenerateRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();

            var userInDb = await _dbContext.Users.FindAsync(user.Id);

            if (userInDb == null) { return null; }

            userInDb.RefreshToken = refreshToken;
            userInDb.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _dbContext.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<TokenResponseDto> CreateTokens(User user)
        {
            return new TokenResponseDto()
            {
                JWTToken = GenerateToken(user),
                RefreshToken = await SaveAndGenerateRefreshTokenAsync(user),
                UserId = user.Id,
                Email = user.Email
            };
        }

        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return null;

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return null;
            }

            return user;
        }


    }
}
