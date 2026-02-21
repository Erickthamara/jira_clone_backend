using jira_clone_backend.Data;
using jira_clone_backend.DTO;
using jira_clone_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jira_clone_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private JiraContext _dbContext;

        public UserService(JiraContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserResponse> AddUserAsync(UserResponse NewUser)
        {
            if (NewUser == null) throw new ArgumentNullException(nameof(NewUser));

            var NewUserResponse = new User
            {
                Email = NewUser.Email,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                AvatarUrl = NewUser.AvatarUrl,
                PasswordHash = NewUser.Password,
                UserName = NewUser.UserName,
            };

            if (NewUserResponse != null)
                await _dbContext.Users.AddAsync(NewUserResponse);

            await _dbContext.SaveChangesAsync();

            return new UserResponse
            {
                UserName = NewUser.UserName,
                Email = NewUser.Email,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                AvatarUrl = NewUser.AvatarUrl,
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            User userToDelete = await _dbContext.Users.FindAsync(id);

            if (userToDelete != null) return false;

            userToDelete.IsActive = false;
            userToDelete.DeactivatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            List<UserResponse> userResponses = [];
            userResponses = await _dbContext.Users.Select(p => new UserResponse
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                AvatarUrl = p.AvatarUrl,
                UserName = p.UserName,
                Email = p.Email,
            }).ToListAsync();

            return userResponses;
        }

        public async Task<UserResponse> GetSingleUserByIdAsync(int Id)
        {
            User user = await _dbContext.Users.FindAsync(Id);

            if (user == null) return new UserResponse { };

            return new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUrl = user.AvatarUrl,
                UserName = user.UserName,
            };
        }

        public async Task<bool> UpdateUserAsync(int Id, UserResponse UserObject)
        {
            User user = await _dbContext.Users.FindAsync(Id);

            if (user == null) return false;

            user.FirstName = UserObject.FirstName;
            user.LastName = UserObject.LastName;
            user.AvatarUrl = UserObject.AvatarUrl;
            user.UserName = UserObject.UserName;
            user.Email = UserObject.Email;


            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
