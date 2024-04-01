using RecipeSiteBackend.Models;
using System.Security.Claims;

namespace RecipeSiteBackend.Auth
{
    public interface IJWTManager
    {
        Tokens GenerateToken(string userName, string firstName);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
