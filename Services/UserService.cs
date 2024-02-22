using RecipeSiteBackend.Models;
using Microsoft.EntityFrameworkCore;
using RecipeSiteBackend.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using RecipeSiteBackend.Validation;

namespace RecipeSiteBackend.Services;

public class UserService
{
    private readonly RecipesDbContext _context;
    private readonly IConfiguration _configuration;
    public UserService(RecipesDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Guid> getUUIDFromToken(string token)
    {
        Guid uuid = Guid.Empty;
        if (token.StartsWith("Bearer"))
        {
            token = token.Substring("Bearer ".Length).Trim();
        }
        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        var claims = new Dictionary<string, string>();

        foreach (var claim in jwt.Claims)
        {
            claims.Add(claim.Type, claim.Value);
        }

        string username = claims["unique_name"];
        if(username != null)
        {
            var user = await GetByUsername(username);
            if(user != null)
            {
                return user.UUID;
            }
        }
        return uuid;
    }

    public async Task<User?> GetByUUID(Guid uuid)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.UUID == uuid);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> CreateUser(User newUser)
    {
        newUser.Recipes = null;
        newUser.Policies = null;
        newUser.Ratings = null;
        newUser.UUID = Guid.Empty;
        if (string.IsNullOrEmpty(newUser.Lastname))
        {
            newUser.Lastname = null;
        }
        var plainPassword = newUser.Password;
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        newUser.Password = hashPassword;

        //generate uuid for user
        //var uuidExists = true;
        //string newUUID = Guid.NewGuid().ToString();
        //while (uuidExists)
        //{
        //    var uuid = Guid.NewGuid().ToString();
        //    if (await GetByUUID(uuid) == null)
        //    {
        //        newUUID = uuid;
        //        uuidExists = false;
        //    }
        //}

        //newUser.UUID = newUUID;        
        newUser.CreatedDate = DateTime.Now.ToUniversalTime();
        newUser.LastModifiedDate = DateTime.Now.ToUniversalTime();

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new User {
        //UUID = newUUID,
        UUID=newUser.UUID,
        Username=newUser.Username,
        Firstname = newUser.Firstname,
        Lastname = newUser.Lastname,
        Email=newUser.Email
        };
    }

    public async Task<User?> Login(string identifier, string password)
    {
        User? user;
        if(UserValidator.emailIsValid(identifier))
        {
            user = await GetByEmail(identifier);
        }
        else
        {
            user = await GetByUsername(identifier);
        }

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.Firstname),
                new Claim(ClaimTypes.Role, user.Role)
                //other claims
            }),
            IssuedAt = DateTime.UtcNow,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(90),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        return new User 
        {
            Username = user.Username,
            Token = user.Token
        };
    }

}


//syntax for selecting specific fields
//var user = _context.Users
//    .Where(u => u.Id == id)
//    .Select(u=>new User()
//    {
//        Id = u.Id,
//        Username=u.Username,
//        Email = u.Email,
//    })
//    .SingleOrDefault();

//return user;
