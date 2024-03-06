using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(Video_Number), IsUnique = true)]
    public class Recipe_Video
    {
        [Key]
        public Guid UUID { get; set; }
        public int Video_Number { get; set; }
        public string? Url { get; set; }
        public Guid RecipeUUID { get; set; }

        [JsonIgnore]
        public Recipe? Recipe { get; set; }
        public string? Description { get; set; }
    }
}
