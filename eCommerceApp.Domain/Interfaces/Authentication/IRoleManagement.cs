using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface IRoleManagement
    {
        Task<string> GetUserRole(string userEmail);
        Task<bool> AddUserRole(AppUser user, string roleName);
    }
}
