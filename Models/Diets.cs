using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    public class Diets
    {
        public int Id { get; set; } 
        public string? Diet_Name { get; set; }
        [JsonIgnore]
        public ICollection<Recipes>? Recipes { get; set; }
    }
}
