using jira_clone_backend.DTO;
using jira_clone_backend.Services.JWTService;
using jira_clone_backend.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace jira_clone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IJwtService jwtService, IUserService userService)
        {
            _logger = logger;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokenResponse = await _jwtService.RefreshTokenAsync(refreshToken);
            if (tokenResponse == null)
            {
                return Unauthorized();
            }

            return Ok(tokenResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = await _userService.GetSingleUserByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }


            if (user.PasswordHash != loginRequest.Password)
            {
                return Unauthorized("Invalid email or password.");
            }


            var tokenResponse = await _jwtService.CreateTokens(user);
            return Ok(tokenResponse);
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] UserResponse newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var createdUser = await _userService.AddUserAsync(newUser);
            return Ok(createdUser);
        }
    }
}