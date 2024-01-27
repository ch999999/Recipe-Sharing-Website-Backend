using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(UserUUID), nameof(RecipeUUID))]
    public class Rating
    {
        [Column(TypeName ="decimal(2,1)")]
        public decimal Score { get; set; }
        public string? Description { get; set; }
        public Guid UserUUID { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}
