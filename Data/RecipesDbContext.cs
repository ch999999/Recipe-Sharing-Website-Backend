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
    public DbSet<Recipes> Recipes => Set<Recipes>();
    public DbSet<Recipe_Images> Recipe_Images => Set<Recipe_Images>();
}
