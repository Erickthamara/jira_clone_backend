using jira_clone_backend.Data;
using jira_clone_backend.DTO;
using jira_clone_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace jira_clone_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private JiraContext _dbContext;
        private PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();


        public UserService(JiraContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserResponse> AddUserAsync(UserResponse NewUser)
        {
            if (NewUser == null) throw new ArgumentNullException(nameof(NewUser));

            string hashedPassword = _passwordHasher.HashPassword(null, NewUser.Password);

            var NewUserResponse = new User
            {
                Email = NewUser.Email,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                AvatarUrl = NewUser.AvatarUrl,
                PasswordHash = hashedPassword,
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

        public async Task<User> GetSingleUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            return user;

            //return (new User
            //{
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    AvatarUrl = user.AvatarUrl,
            //    UserName = user.UserName,
            //});
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

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            if (user == null) return false;
            return true;
        }

        public async Task<bool> UpdateUserAsync(int Id, UserResponse UserObject)
        {
            var user = await _dbContext.Users.FindAsync(Id);

            if (user == null) return false;

            user.FirstName = UserObject.FirstName;
            user.LastName = UserObject.LastName;
            user.AvatarUrl = UserObject.AvatarUrl;
            user.UserName = UserObject.UserName;
            user.Email = UserObject.Email;


            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserPasswordAsync(int Id, string newPassword)
        {
            var user = await _dbContext.Users.FindAsync(Id);
            if (user == null) return false;
            string hashedPassword = _passwordHasher.HashPassword(null, newPassword);
            user.PasswordHash = hashedPassword;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyUserPasswordAsync(int Id, string password)
        {
            var user = await _dbContext.Users.FindAsync(Id);
            if (user == null) return false;
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }
}
