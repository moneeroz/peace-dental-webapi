using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, int days = 1);
    }
}