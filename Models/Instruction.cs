using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(Sequence_Number), IsUnique = true)]
    public class Instruction
    {
        [Key]
        public Guid UUID { get; set; }
        public int Sequence_Number { get; set; }
        public string? Description { get; set; }
        public Guid RecipeUUID { get; set; }

        [JsonIgnore]
        public Recipe? Recipe { get; set; }

        
        public ICollection<Instruction_Image>? Images { get; set; }
        //public ICollection<Instruction_Video>? Videos { get; set; }
    }
}
