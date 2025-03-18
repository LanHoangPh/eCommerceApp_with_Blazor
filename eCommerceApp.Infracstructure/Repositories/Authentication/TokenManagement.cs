using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infracstructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infracstructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration configuration) : ITokenManagement
    {
        public async Task<int> AddRefeshToken(string userId, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken { UserId = userId, Token = refreshToken });
            return await context.SaveChangesAsync();

        }

        public string GeneratedToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var jwtToekn = tokenHanlder.ReadJwtToken(token);
            if(jwtToekn != null) return jwtToekn.Claims.ToList();
            else return [];

        }

        public async Task<string> GetUserIdByRefeshToken(string refreshToken) => (await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefeshToken(string userId, string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
            if (user == null) return -1;
            user.Token = refreshToken;
            return await context.SaveChangesAsync();

        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
            return user != null;
        }
    }
}
