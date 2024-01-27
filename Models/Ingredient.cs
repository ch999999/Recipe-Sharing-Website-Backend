using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(RecipeUUID),nameof(Ingredient_Number))]
    public class Ingredient
    {
        public int Ingredient_Number { get; set; }
        public Guid RecipeUUID { get; set; }
        public string? Description { get; set; }
        public Recipe? Recipes { get; set; }
    }
}
