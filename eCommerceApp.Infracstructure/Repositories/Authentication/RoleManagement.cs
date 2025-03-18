using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infracstructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserRole(AppUser user, string roleName) => 
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
#pragma warning disable CS8603 // Possible null reference return.
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
