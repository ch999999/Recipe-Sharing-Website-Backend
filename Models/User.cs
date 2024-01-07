using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
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

}
