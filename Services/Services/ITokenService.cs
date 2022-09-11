using Microsoft.AspNetCore.Identity;

namespace Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, IdentityUser user);
        bool ValidateToken(string key, string issuer, string token);
    }
}
