using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Tag_Name { get; set; }

        [JsonIgnore]
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
