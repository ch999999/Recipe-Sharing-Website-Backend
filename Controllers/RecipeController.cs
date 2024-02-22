using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Services;
using System.Reflection.Metadata;
using System.Drawing;
using System.IO;
using RecipeSiteBackend.Validation;


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

        [Authorize]
        [HttpPost]
        [Route("/api/Recipe/AddDescriptionImage")]
        public async Task<IActionResult> UploadDescpImage(Description_Media img)
        {
            if (string.IsNullOrEmpty(img.ImageBase64) || string.IsNullOrEmpty(img.FileExtension))
            {
                return BadRequest(new { ErrorField = "description_image", message = "cannot be nothing" });
            }

            var recipe = await _recipeService.GetByUUID(img.RecipeUUID);
            if (recipe == null) 
            {
                return BadRequest(new { errorField = "description-image", message = "Failed to upload, could not find associated recipe" });
            }

            var imageError = ImageValidator.validateInput(img.ImageBase64, img.FileExtension);
            if(imageError != null)
            {
                return BadRequest(imageError);
            }
            img.Recipe = null;
            var result = await _recipeService.UploadDescriptionMedia(img);
            if (string.IsNullOrEmpty(result.Url))
            {
                return BadRequest(new { errorField = "description_image", message = "Failed to upload" });
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/Recipe/AddInstructionImage")]
        public async Task<IActionResult> UploadInstrImage(Instruction_Image img)
        {
            throw new NotImplementedException();
        }

        [EnableCors]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe newRecipe)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.getUUIDFromToken(token);
            if(ownerUUID == Guid.Empty)
            {
                return NotFound(new {Error = "User not found. Try Relogging." });
            }
            newRecipe.OwnerUUID = ownerUUID;
            var recipe = await _recipeService.CreateRecipe(newRecipe);
            return Ok(recipe);
        }

        [HttpPost]
        [Route("/api/Recipe/validate")]
        public async Task<IActionResult> ValidateRecipe(Recipe newRecipe)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/Recipe/difficulties")]
        public async Task<IActionResult> GetDifficulties()
        {
            var diffs = await _recipeService.GetDifficulties();
            if(diffs == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(diffs);
            }
        }

        [HttpGet]
        [Route("/api/Recipe/diets")]
        public async Task<IActionResult> GetDiets()
        {
            var diets = await _recipeService.GetDiets();
            if(diets == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(diets);
            }
        }

        [HttpGet]
        [Route("/api/Recipe/cuisines")]
        public async Task<IActionResult> GetCuisines()
        {
            var cuisines = await _recipeService.GetCuisines();
            if(cuisines == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cuisines);
            }
        }

        [HttpGet]
        [Route("/api/Recipe/tags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _recipeService.GetTags();
            if (tags == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tags);
            }
        }
    }
}
