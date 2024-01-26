using Microsoft.EntityFrameworkCore;
using RecipeSiteBackend.Models;
using System.Dynamic;

namespace RecipeSiteBackend.Data;

public class RecipesDbContext : DbContext
{
    public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Recipe_Image> Recipe_Images => Set<Recipe_Image>();
    public DbSet<Recipe_Video> Recipe_Videos => Set<Recipe_Video>();
    public DbSet<Diet> Diets => Set<Diet>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Permitted_User> Policies => Set<Permitted_User>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Tag> Tags => Set<Tag>();
}
