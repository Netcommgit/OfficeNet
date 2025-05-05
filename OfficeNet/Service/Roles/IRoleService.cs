using Microsoft.AspNetCore.Identity;
using OfficeNet.Domain.Contracts;

namespace OfficeNet.Service.Roles
{
    public interface IRoleService
    {
        Task<IdentityRole> AddRoleAsync(string roleName);
        Task<UserRole> AssingRoleToUserAync(UserRole user);
    }


}
