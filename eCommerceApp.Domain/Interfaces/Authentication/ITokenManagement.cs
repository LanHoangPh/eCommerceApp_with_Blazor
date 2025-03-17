using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface ITokenManagement 
    {
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> GetUserIdByRefeshToken(string refreshToken);
        Task<int> AddRefeshToken(string userId, string refreshToken);
        Task<int> UpdateRefeshToken(string userId, string refreshToken);
        string GeneratedToken(List<Claim> claims);
    }

}
