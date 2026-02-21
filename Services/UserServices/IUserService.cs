using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();

        Task<UserResponse> GetSingleUserByIdAsync(int Id);

        Task<UserResponse> AddUserAsync(UserResponse NewUser);

        Task<bool> UpdateUserAsync(int Id, UserResponse UserObject);

        Task<bool> DeleteUserAsync(int id);
    }
}
