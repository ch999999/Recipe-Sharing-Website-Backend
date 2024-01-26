using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(UserUUID),nameof(RecipeUUID))]
    public class Permitted_User
    {
        public string? Permission_Level { get; set; }
        public string? UserUUID { get; set; }
        public string? RecipeUUID { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
       
    }
}
