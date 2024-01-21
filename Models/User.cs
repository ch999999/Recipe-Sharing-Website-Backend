﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
[Index(nameof(UUID), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string? Username { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? UUID { get; set; }

    public string? Role { get; set; }

    [NotMapped]
    public string? Token { get; set; }

}

public class LoginUser
{
    public string? Identifier { get; set; }
    public string? Password { get; set; }
}
