using Microsoft.EntityFrameworkCore;
using RecipeSiteBackend.Data;
using RecipeSiteBackend.Models;

namespace RecipeSiteBackend.Services
{
    public class RecipeService
    {
        private readonly RecipesDbContext _context;
        private readonly IConfiguration _configuration;
        
        public RecipeService(RecipesDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //public async Task<Recipe>? GetByUUID(Guid uuid)
        //{
        //    return await _context.Recipes
        //        .Include(r => r.Tags)
        //        .Include(r => r.Diets)
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(r => r.UUID == uuid);
        //}

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            recipe.UUID = Guid.Empty;
            recipe.Owner = null;
            recipe.Ratings = null;
            recipe.Cuisine = null;

            List<Diet>? dietList = recipe.Diets?.ToList();
            List<Tag>? tagList = recipe.Tags?.ToList();
            recipe.Diets = null;
            recipe.Tags = null;
            
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            var recipeToUpdate = _context.Recipes.Find(recipe.UUID);

            //Add diets
            if (dietList != null)
            {
                foreach (Diet diet in dietList)
                {
                    var dietToAdd = _context.Diets.Find(diet.Id);
                    if (dietToAdd != null)
                    {
                        if(recipeToUpdate.Diets is null)
                        {
                            recipeToUpdate.Diets = new List<Diet>();
                        }
                        recipeToUpdate.Diets.Add(dietToAdd);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            //Add tags
            if(tagList != null)
            {
                foreach(Tag tag in tagList)
                {
                    var tagToAdd = _context.Tags.Find(tag.Id);
                    if(tagToAdd != null)
                    {
                        if(recipeToUpdate.Tags is null)
                        {
                            recipeToUpdate.Tags = new List<Tag>();
                        }
                        recipeToUpdate.Tags.Add(tagToAdd);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            

            return new Recipe
            {
                Title = recipe.Title,
            };
        }

        
    }
}
