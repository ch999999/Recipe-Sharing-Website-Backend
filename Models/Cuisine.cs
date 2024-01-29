using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(Cuisine_Name), IsUnique = true)]
    public class Cuisine
    {
        public int Id { get; set; }
        public string? Cuisine_Name { get; set; }
    }
}
