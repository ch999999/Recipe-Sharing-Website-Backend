﻿using RecipeSiteBackend.Models;
using Microsoft.EntityFrameworkCore;
using RecipeSiteBackend.Data;
//using System.Security.Cryptography;
//using Microsoft.AspNetCore.DataProtection.XmlEncryption;
//using System.Data;
//using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using BCrypt.Net;
using RecipeSiteBackend.Validation;

namespace RecipeSiteBackend.Services;

public class UserService
{
    private readonly RecipesDbContext _context;
    //private readonly IConfiguration _configuration;
    public UserService(RecipesDbContext context, IConfiguration configuration)
    {
        _context = context;
        //_configuration = configuration;
    }

    public async Task<Guid> GetUUIDFromToken(string token) //formerly getUUIDFromToken
    {
        //Guid uuid = Guid.Empty;
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
            var user = await GetUserByUsername(username);
            if(user != null)
            {
                return user.UUID;
            }
        }
        return Guid.Empty;
    }

    //public async Task<User?> GetUserByUUID(Guid uuid) //formerly GetByUUID
    //{
    //    return await _context.Users
    //        .AsNoTracking()
    //        .SingleOrDefaultAsync(u => u.UUID == uuid);
    //}

    public async Task<User?> GetUserByEmail(string email) //formerly GetByEmail
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByUsername(string username) //formerly GetByUsername
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> CreateUser(User newUser)
    {
        newUser.Recipes = null;
        newUser.Policies = null;
        //newUser.Ratings = null;
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
        //UUID=newUser.UUID,
        Username=newUser.Username,
        //Firstname = newUser.Firstname,
        //Lastname = newUser.Lastname,
        //Email=newUser.Email
        };
    }

    public async Task<User> IsValidUser(string identifier, string password)
    {
        User? user;
        if (UserValidator.emailIsValid(identifier))
        {
            user = await GetUserByEmail(identifier);
        }
        else
        {
            user = await GetUserByUsername(identifier);
        }

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }
        user.Password = string.Empty;
        return user;
    }

    public async Task<User> UpdateUser(User updatedUser, Guid userUUID)
    {
        try
        {
            var userToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.UUID == userUUID);
            userToUpdate.Username = updatedUser.Username;
            userToUpdate.Email = updatedUser.Email;
            userToUpdate.Firstname = updatedUser.Firstname;
            userToUpdate.Lastname = updatedUser.Lastname;
            userToUpdate.LastModifiedDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User> UpdateUserPassword(string newPassword, Guid userUUID)
    {
        try
        {
            var userToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.UUID == userUUID);
            var plainPassword = newPassword;
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            userToUpdate.Password = hashPassword;
            userToUpdate.LastModifiedDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
        catch
        {
            return null;
        }
    }

    //public async Task<User?> Login(string identifier, string password)
    //{
    //    User? user;
    //    if(UserValidator.emailIsValid(identifier))
    //    {
    //        user = await GetUserByEmail(identifier);
    //    }
    //    else
    //    {
    //        user = await GetUserByUsername(identifier);
    //    }

    //    if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
    //    {
    //        return null;
    //    }

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new Claim[]
    //        {
    //            new Claim(ClaimTypes.Name, user.Username),
    //            new Claim(ClaimTypes.GivenName, user.Firstname),
    //            new Claim(ClaimTypes.Role, user.Role)
    //            //other claims
    //        }),
    //        IssuedAt = DateTime.UtcNow,
    //        Issuer = _configuration["JWT:Issuer"],
    //        Audience = _configuration["JWT:Audience"],
    //        Expires = DateTime.UtcNow.AddMinutes(90),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

    //    };

    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    user.Token = tokenHandler.WriteToken(token);
    //    return new User 
    //    {
    //        Username = user.Username,
    //        Token = user.Token
    //    };
    //}

    public UserRefreshToken AddUserRefreshTokens(UserRefreshToken refreshToken)
    {
        _context.UserRefreshTokens.Add(refreshToken);
        _context.SaveChanges();
        return refreshToken;
    }

    public void DeleteUserRefreshTokens(Guid userUUID, string refreshToken)
    {
        var item = _context.UserRefreshTokens.FirstOrDefault(x => x.UserUUID == userUUID && x.RefreshToken == refreshToken);
        if (item != null)
        {
            _context.UserRefreshTokens.Remove(item);
        }
    }

    //SaveChanges method to finish off methods that do not confirm save changes immediately
    public void SaveChanges() 
    {
        _context.SaveChanges();
    }

    public UserRefreshToken GetSavedRefreshTokens( Guid userUUID, string refreshToken)
    {
        var token = _context.UserRefreshTokens.FirstOrDefault(x => x.UserUUID == userUUID && x.RefreshToken == refreshToken);
        
        if(token == null)
        {
            return null;
        }

        if(token.ValidUntil > DateTime.UtcNow)
        {
            return token;
        }
        else
        {
            _context.UserRefreshTokens.Remove(token);
            _context.SaveChanges();
            return null;
        }

    }

    public async Task<User> GetUserByUUID(Guid userUUID)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UUID == userUUID);
        if (user == null)
        {
            return null;
        }
        else
        {
            user.Password = null;
            return user;
        }
    }

    //public async Task<User?> Login2(string identifier, string password)
    //{
    //    User? user;
    //    if (UserValidator.emailIsValid(identifier))
    //    {
    //        user = await GetUserByEmail(identifier);
    //    }
    //    else
    //    {
    //        user = await GetUserByUsername(identifier);
    //    }

    //    if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
    //    {
    //        return null;
    //    }

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new Claim[]
    //        {
    //            new Claim(ClaimTypes.Name, user.Username),
    //            new Claim(ClaimTypes.GivenName, user.Firstname),
    //            new Claim(ClaimTypes.Role, user.Role)
    //            //other claims
    //        }),
    //        IssuedAt = DateTime.UtcNow,
    //        Issuer = _configuration["JWT:Issuer"],
    //        Audience = _configuration["JWT:Audience"],
    //        Expires = DateTime.UtcNow.AddMinutes(90),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

    //    };

    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    user.Token = tokenHandler.WriteToken(token);
    //    return new User
    //    {
    //        Username = user.Username,
    //        Token = user.Token
    //    };
    //}



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
