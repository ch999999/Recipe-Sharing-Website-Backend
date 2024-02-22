using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(InstructionUUID),nameof(Image_Number), IsUnique = true)]
    public class Instruction_Image
    {
        [Key]       
        public Guid UUID { get; set; }
        public int Image_Number { get; set; }
        public string? Url { get; set; }
        public Guid InstructionUUID { get; set; }
        public Instruction? Instruction { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public Base64StringAttribute? imageBase64 { get; set; }

    }
}
