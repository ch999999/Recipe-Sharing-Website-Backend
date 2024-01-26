using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    public class Recipes
    {
        public int Id { get; set; } 
        public string? UUID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Difficulty { get; set; }
        public int Prep_Time_Mins { get; set; }
        public int Cook_Time_Mins { get; set; }
        public int Servings { get; set; }
        public string? Cuisine { get; set; }
        public string? Meal_Type { get; set; }
        public string? Source { get; set; }

        [Column(TypeName="decimal(2,1)")]
        public decimal Rating { get; set; }
        public User? Owner { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastMofifiedDate { get; set; }
        public bool IsViewableByPublic { get; set; }
        





    }
}
