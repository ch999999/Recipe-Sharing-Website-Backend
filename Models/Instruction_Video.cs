using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(InstructionUUID), nameof(Video_Number))]
    public class Instruction_Video
    {
        public int Video_Number { get; set; }
        public string? Url { get; set; }
        public Guid InstructionUUID { get; set; }
        public Instruction? Instruction { get; set; }
        public string? Description { get; set; }
    }
}
