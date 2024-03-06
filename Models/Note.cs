using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(Note_Number), IsUnique = true)]
    public class Note
    {
        [Key]
        public Guid UUID { get; set; }
        public int Note_Number { get; set; }
        public string? Description { get; set; }
        public Guid RecipeUUID { get; set; }
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}
