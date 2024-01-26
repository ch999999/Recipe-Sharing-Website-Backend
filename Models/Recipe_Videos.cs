using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    public class Recipe_Videos
    {
        [Key]
        public string? UUID { get; set; }
        public string? Url { get; set; }
        public Recipes? Recipe { get; set; }
        public string? Description { get; set; }
    }
}
