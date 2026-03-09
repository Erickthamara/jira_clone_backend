using jira_clone_backend.DTO;
using jira_clone_backend.Models;

namespace jira_clone_backend.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();

        Task<UserResponse> GetSingleUserByIdAsync(int Id);

        Task<User> GetSingleUserByEmailAsync(string email);

        Task<UserResponse> AddUserAsync(UserResponse NewUser);

        Task<bool> UpdateUserAsync(int Id, UserResponse UserObject);

        Task<bool> DeleteUserAsync(int id);

        Task<bool> LoginUserAsync(string email, string password);
    }
}
