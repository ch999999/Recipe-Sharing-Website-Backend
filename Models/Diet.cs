using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    public class Diet
    {
        public int Id { get; set; } 
        public string? Diet_Name { get; set; }

        [JsonIgnore]
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
