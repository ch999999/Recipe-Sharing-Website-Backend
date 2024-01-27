using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(RecipeUUID), nameof(Note_Number))]
    public class Note
    {
        public int Note_Number { get; set; }
        public string? Description { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
