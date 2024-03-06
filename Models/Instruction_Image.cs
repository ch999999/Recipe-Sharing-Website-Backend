using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(InstructionUUID),nameof(Image_Number), IsUnique = true)]
    public class Instruction_Image
    {
        [Key]       
        public Guid UUID { get; set; }
        public int Image_Number { get; set; }
        public string? Url { get; set; }
        public string? Filename { get; set; }
        public Guid InstructionUUID { get; set; }

        [JsonIgnore]
        public Instruction? Instruction { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public string? ImageBase64 { get; set; }
        [NotMapped]
        public string? FileExtension { get; set; }

    }
}
