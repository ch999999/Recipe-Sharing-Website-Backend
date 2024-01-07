using RecipeSiteBackend.Models;
using Microsoft.EntityFrameworkCore;
using RecipeSiteBackend.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Data;

namespace RecipeSiteBackend.Services;

public class UserService
{
    private readonly RecipesDbContext _context;
    public UserService(RecipesDbContext context)
    {
        _context = context;
    }

    public User? CreateUser(User newUser)
    {
        var plainPassword = newUser.Password;
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        newUser.Password = hashPassword;
      
        _context.Users.Add(newUser);
        _context.SaveChanges();
 

        return new User {
        Id = newUser.Id,
        Username=newUser.Username,
        Email=newUser.Email
        };
    }

}
