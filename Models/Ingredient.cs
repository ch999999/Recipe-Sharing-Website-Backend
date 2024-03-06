using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(Ingredient_Number), IsUnique = true)]
    public class Ingredient
    {
        [Key]
        public Guid UUID { get; set; }
        public int Ingredient_Number { get; set; }
        public Guid RecipeUUID { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public Recipe? Recipes { get; set; }
    }
}
