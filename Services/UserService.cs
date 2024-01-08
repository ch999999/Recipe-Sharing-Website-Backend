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

    public User? GetById(int id)
    {
        return _context.Users
            .AsNoTracking()
            .SingleOrDefault(u => u.Id == id);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users
            .AsNoTracking()
            .SingleOrDefault(u => u.Email == email);
    }

    public User? GetByUsername(string username)
    {
        return _context.Users
            .AsNoTracking()
            .SingleOrDefault(u => u.Username == username);
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
