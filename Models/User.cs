using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public string? UUID { get; set; }

    [Required]
    [MaxLength(64)]
    public string? Username { get; set; }

    [Required]
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }


    public string? Role { get; set; }

    public DateTime CreatedDate { get; set; } 
    public DateTime LastModifiedDate { get; set; }
    public ICollection<Recipes>? Recipes { get; set; }

    [NotMapped]
    public string? Token { get; set; }

}

public class LoginUser
{
    public string? Identifier { get; set; }
    public string? Password { get; set; }
}
