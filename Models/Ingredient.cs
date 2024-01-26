using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    public class Ingredient
    {
        [Key]
        public string? UUID { get; set; }

        public string? Description { get; set; }
        public Recipe? Recipes { get; set; }
    }
}
