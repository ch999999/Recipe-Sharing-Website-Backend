﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteBackend.Models
{
    [Index(nameof(RecipeUUID), nameof(UserUUID), IsUnique = true)]
    public class Rating
    {
        [Key]
        public Guid UUID { get; set; }
        [Column(TypeName ="decimal(2,1)")]
        public decimal Score { get; set; }
        public string? Description { get; set; }
        public Guid UserUUID { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}
