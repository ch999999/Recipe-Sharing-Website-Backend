using Microsoft.EntityFrameworkCore;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(RecipeUUID), nameof(Sequence_Number))]
    public class Instruction
    {
        public int Sequence_Number { get; set; }
        public string? Description { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        public ICollection<Instruction_Image>? Images { get; set; }
        public ICollection<Instruction_Video>? Videos { get; set; }
    }
}
