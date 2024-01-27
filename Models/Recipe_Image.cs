﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeSiteBackend.Models
{
    [PrimaryKey(nameof(RecipeUUID), nameof(Image_Number))]
    public class Recipe_Image
    {
        public int Image_Number { get; set; }
        public string? Url { get; set; }
        public Guid RecipeUUID { get; set; }
        public Recipe? Recipe { get; set; }
        public string? Description {  get; set; }
    }
}
