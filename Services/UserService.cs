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

    public async Task<User?> GetById(int id)
    {
        //return await _context.Users.AsNoTracking().SingleOrDefaultAsync();
        
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id);
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
        var plainPassword = newUser.Password;
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        newUser.Password = hashPassword;
      
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new User {
        Id = newUser.Id,
        Username=newUser.Username,
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
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                //other claims
            }),
            IssuedAt = DateTime.UtcNow,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        return new User 
        {
            Username = user.Username,
            Email = user.Email,
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
