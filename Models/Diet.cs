using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(Diet_Name), IsUnique = true)]
    public class Diet
    {
        public int Id { get; set; } 
        public string? Diet_Name { get; set; }

        [JsonIgnore]
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
