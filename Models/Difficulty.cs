using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(Difficulty_Name), IsUnique = true)]
    public class Difficulty
    {
        public int Id { get; set; }
        public string? Difficulty_Name { get; set; } 
    }
}
