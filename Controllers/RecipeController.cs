using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Services;

namespace RecipeSiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;
        private readonly UserService _userService;

        public RecipeController(RecipeService recipeService, UserService userService) 
        { 
            _recipeService = recipeService;
            _userService = userService;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe newRecipe)
        {
            if(_userService.GetByUUID(newRecipe.OwnerUUID)==null)
            {
                return BadRequest(new {errorField= "user", message="user does not exist. Try relogging." });
            }
            var recipe = await _recipeService.CreateRecipe(newRecipe);
            return Ok(recipe);
        }
    }
}
