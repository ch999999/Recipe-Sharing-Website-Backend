using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(61)]
    public string? UserName { get; set; }

    [Required]
    public string? UserEmail { get; set; }

    [Required]
    public string? Password { get; set; }
}
