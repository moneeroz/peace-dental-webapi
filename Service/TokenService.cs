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
            SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!));

            IList<string>? roles = await _userManager.GetRolesAsync(user);

            List<Claim>? claims =
            [
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (ClaimTypes.Role, roles.First()),
                new ("userId", user.Id)
            ];

            SigningCredentials? creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor? tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(days),
                SigningCredentials = creds,
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}