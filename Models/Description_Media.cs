using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    public class Description_Media
    {
        [Key]
        public Guid UUID { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? Filetype { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        [NotMapped]
        public string? ImageBase64 { get; set; }
        [NotMapped]
        public string? FileExtension { get; set; }
    }
}
