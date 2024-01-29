using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(UserUUID), IsUnique = true)]
    public class Permitted_User
    {
        [Key]
        public Guid UUID { get; set; }
        public string? Permission_Level { get; set; }
        public Guid UserUUID { get; set; }
        public Guid RecipeUUID { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
       
    }
}
