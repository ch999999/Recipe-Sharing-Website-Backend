using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(Image_Number), IsUnique = true)]
    public class Recipe_Image
    {
        [Key]
        public Guid UUID { get; set; }
        public int Image_Number { get; set; }
        public string? Url { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        public string? Description {  get; set; }
    }
}
