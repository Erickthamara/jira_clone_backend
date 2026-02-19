using jira_clone_backend.DTO;

namespace jira_clone_backend.Services.User
{
    public class UserService : IUserService
    {
        public Task<UserResponse> AddUserAsync(UserLoginResponse NewUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserResponse>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetSingleUserByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserResponse UserObject)
        {
            throw new NotImplementedException();
        }
    }
}
