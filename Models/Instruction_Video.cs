using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(InstructionUUID), nameof(Video_Number), IsUnique = true)]
    public class Instruction_Video
    {
        [Key]
        public Guid UUID { get; set; }
        public int Video_Number { get; set; }
        public string? Url { get; set; }
        public Guid InstructionUUID { get; set; }

        [JsonIgnore]
        public Instruction? Instruction { get; set; }
        public string? Description { get; set; }
    }
}
