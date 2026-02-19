using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.User
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();

        Task<UserResponse> GetSingleUserByIdAsync(int Id);

        Task<UserResponse> AddUserAsync(UserLoginResponse NewUser);

        Task<bool> UpdateUserAsync(UserResponse UserObject);

        Task<bool> DeleteUserAsync(int id);
    }
}
