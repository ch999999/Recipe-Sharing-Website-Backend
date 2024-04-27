using Microsoft.IdentityModel.Tokens;
using RecipeSiteBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeSiteBackend.Auth
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration _configuration;
        public JWTManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens? GenerateToken(string username , string firstname)
        {
            return GenerateJWTTokens(username, firstname);
        }

        public Tokens? GenerateJWTTokens(string userName, string firstName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration["JWT:SecretKey"];
                if (secretKey == null)
                {
                    return null;
                }
                var tokenKey = Encoding.UTF8.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.GivenName, firstName),
                        new Claim(ClaimTypes.Role, "Normal")
                    }),
                    IssuedAt = DateTime.UtcNow,
                    Issuer = _configuration["JWT:Issuer"],
                    Audience = _configuration["JWT:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };     
            }catch
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = _configuration["JWT:SecretKey"];
            if (secretKey == null)
            {
                throw new SecurityTokenException("Invalid token");
            }
            var Key = Encoding.UTF8.GetBytes(secretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero,
                ValidAudience = _configuration["JWT:Audience"],
                ValidIssuer = _configuration["JWT:Issuer"]
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var principal = TokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
    }
}
