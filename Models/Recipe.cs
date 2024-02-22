using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    public class Recipe
    {
        [Key]
        public Guid UUID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Prep_Time_Mins { get; set; }
        public int Cook_Time_Mins { get; set; }
        public int Servings { get; set; }
        public string? Meal_Type { get; set; }
        public string? Source { get; set; }
        public Guid OwnerUUID { get; set; }
        public User? Owner { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsViewableByPublic { get; set; }
        public int CuisineId { get; set; }
        public Cuisine? Cuisine { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty? Difficulty { get; set; }
       
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Diet>? Diets { get; set; }
        public ICollection<Recipe_Image>? Images { get; set; }
        public ICollection<Recipe_Video>? Videos { get; set; }
        
        public ICollection<Ingredient>? Ingredients { get; set;}
        public ICollection<Permitted_User>? Policies { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        
        public ICollection<Note>? Notes { get; set; }
        public ICollection<Instruction>? Instructions { get; set; }
        

    }
}
