using Microsoft.AspNetCore.Identity;
using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using System.Runtime.Intrinsics.X86;

namespace OfficeNet.Service.Roles
{
    public class RoleImple : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleImple(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityRole> AddRoleAsync(string roleName)
        {
            var role = new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            return role;
        }

       

        public async Task<UserRole> AssingRoleToUserAync(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(userRole.RoleId))
                throw new Exception("Role does not exist");

            var result = await _userManager.AddToRoleAsync(user, userRole.RoleId);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign role: {errors}");
            }

            return userRole;
        }
    }
}
