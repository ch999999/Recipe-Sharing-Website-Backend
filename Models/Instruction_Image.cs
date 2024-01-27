using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(InstructionUUID),nameof(Image_Number))]
    public class Instruction_Image
    {
        public int Image_Number { get; set; }
        public string? Url { get; set; }
        public Guid InstructionUUID { get; set; }
        public Instruction? Instruction { get; set; }
        public string? Description { get; set; }
    }
}
