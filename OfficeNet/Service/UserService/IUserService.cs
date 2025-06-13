using Microsoft.AspNetCore.Identity;
using OfficeNet.Domain.Contracts;

namespace OfficeNet.Service.UserService
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(UserRegisterRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
        Task DeleteAsync(Guid id);
        Task<RevokeRefreshTokenResponse> RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<CurrentUserResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);

        Task<UserResponse> LoginAsync(UserLoginRequest loginRequest);

        Task<List<UserResponse>> GetUserListAsync();
        Task<List<UserResponse>> GetUserListByPlantDept(int plantId, int departmentId);
    }   
}
