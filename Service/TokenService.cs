using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotenv.net;
using Microsoft.IdentityModel.Tokens;
using peace_api.Interfaces;
using peace_api.Models;
using Microsoft.AspNetCore.Identity;

namespace peace_api.Service
{
    public class TokenService(UserManager<AppUser> userManager) : ITokenService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        public async Task<string> CreateToken(AppUser user, int days)
        {
            DotEnv.Load();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!));

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (ClaimTypes.Role, roles.First()),
                new ("userId", user.Id)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(days),
                SigningCredentials = creds,
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}