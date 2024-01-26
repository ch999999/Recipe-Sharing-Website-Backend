namespace RecipeSiteBackend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Tag_Name { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
