using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    public class UserRefreshToken
    {
        [Key]
        public Guid UUID { get; set; }

        [Required]
        public Guid UserUUID { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public DateTime ValidUntil { get; set; } = DateTime.UtcNow.AddMonths(1);

        
    }
}
