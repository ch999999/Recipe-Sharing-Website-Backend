using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(UserUUID),nameof(RecipeUUID))]
    public class Permitted_User
    {
        public string? Permission_Level { get; set; }
        public Guid UserUUID { get; set; }
        public Guid RecipeUUID { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
       
    }
}
