using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Services;
//using System.Reflection.Metadata;
//using System.Drawing;
//using System.IO;
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
        [HttpPut]
        [Route("/api/Recipe/UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe(Recipe newRecipe)
        {
            var token = Request.Headers["Authorization"];
            if (token.ToString() == null)
            {
                return NotFound();
            }
            var validationError = RecipeValidator.validateInput(newRecipe);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            var recipeToUpdate = await _recipeService.GetRecipeByUUID(newRecipe.UUID);
            if(recipeToUpdate == null)
            {
                return NotFound();
            }
            if (recipeToUpdate.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
            }

            foreach(Instruction instruction in newRecipe.Instructions)
            {
                if (instruction.Images.Count > 0)
                {
                    if(instruction.Images.Count > 1)
                    {
                        return BadRequest("Invalid operation. Only one image allowed per instruction"); //keep instruction-instruction_images as one to many relationship for now
                    }
                    var images = instruction.Images.ToList();
                    var urlExistsInInstructions = await _recipeService.FindImageByUrl(images[0].Url);
                    var urlExistsInDescription = await _recipeService.FindDescriptionMediaByUrl(images[0].Url);
                    if(urlExistsInInstructions == null && urlExistsInDescription==null && !newRecipe.ExistingUrls.Contains(images[0].Url))
                    {
                        return BadRequest(new {error = "Invalid Url", problemChild = images[0].Url });
                    }
                }
            }
            var updatedRecipe = await _recipeService.UpdateRecipe(newRecipe);
            if (updatedRecipe == null)
            {
                return BadRequest();
            }
            return Ok(updatedRecipe);
        }

        [Authorize]
        [HttpDelete("/api/Recipe/Delete/{UUID}")]
        public async Task<IActionResult> DeleteRecipe(Guid UUID)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            var recipeToDelete = await _recipeService.GetRecipeByUUID(UUID);
            if (recipeToDelete == null)
            {
                return NotFound();
            }
            if (recipeToDelete.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
            }
            var result = await _recipeService.DeleteRecipe(recipeToDelete);
            if (result != null)
            {
                return BadRequest(result);
            }
            return Ok();

        }

        [Authorize]
        [HttpDelete("/api/Recipe/DescriptionImage/{UUID}")]
        public async Task<IActionResult> DeleteDescpImage(Guid UUID)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            Description_Media mediaToDelete = await _recipeService.GetDescriptionMedia(UUID);

            if (mediaToDelete != null)
            {
                var recipe = await _recipeService.GetRecipeByUUID(mediaToDelete.RecipeUUID);
                if(recipe.OwnerUUID!= ownerUUID)
                {
                    return Unauthorized();
                }
                _recipeService.DeleteDescriptionMedia(mediaToDelete);
                return Ok();
            }
            return NotFound();

        }

        [Authorize]
        [HttpPost]
        [Route("/api/Recipe/UpdateUploadDescriptionImage")]
        public async Task<IActionResult> UploadUpdatedDescpImage(Description_Media img)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            if (string.IsNullOrEmpty(img.ImageBase64) || string.IsNullOrEmpty(img.FileExtension))
            {
                return BadRequest(new { ErrorField = "description_image", message = "cannot be nothing" });
            }

            var recipe = await _recipeService.GetRecipeByUUID(img.RecipeUUID);
            if (recipe == null)
            {
                return BadRequest(new { errorField = "description-image", message = "Failed to upload, could not find associated recipe" });
            }
            if(recipe.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
            }

            var imageError = ImageValidator.validateInput(img.ImageBase64, img.FileExtension);
            if (imageError != null)
            {
                return BadRequest(imageError);
            }

            var mediaToDelete = await _recipeService.GetDescriptionMedia(img.RecipeUUID);
            if (mediaToDelete != null)
            {
                _recipeService.DeleteDescriptionMedia(mediaToDelete);
            }

            img.Recipe = null;
            var result = await _recipeService.UploadDescriptionMedia(img);
            if (string.IsNullOrEmpty(result.Url))
            {
                return BadRequest(new { errorField = "description_image", message = "Failed to upload" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/Recipe/UpdateDescriptionImage")]
        public async Task<IActionResult> UpdateDescpImage(Description_Media img)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            if (string.IsNullOrEmpty(img.Url))
            {
                return BadRequest(new { ErrorField = "description_image", message = "cannot be nothing" });
            }
            var recipe = await _recipeService.GetRecipeByUUID(img.RecipeUUID);
            if (recipe == null)
            {
                return BadRequest(new { errorField = "description-image", message = "Failed to upload, could not find associated recipe" });
            }
            if (recipe.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
            }
            var existsInInstructions = await _recipeService.FindImageByUrl(img.Url);
            var existsInDescriptionMedia = await _recipeService.FindDescriptionMediaByUrl(img.Url);

            if (existsInDescriptionMedia == null && existsInInstructions == null && !img.ExistingUrls.Contains(img.Url))
            {
                return BadRequest(new { error = "Invalid Url" });
            }

            var result = await _recipeService.UpdateDescriptionMediaByUrl(img);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/Recipe/AddDescriptionImage")]
        public async Task<IActionResult> UploadDescpImage(Description_Media img)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            if (string.IsNullOrEmpty(img.ImageBase64) || string.IsNullOrEmpty(img.FileExtension))
            {
                return BadRequest(new { ErrorField = "description_image", message = "cannot be nothing" });
            }

            var recipe = await _recipeService.GetRecipeByUUID(img.RecipeUUID);
            if (recipe == null) 
            {
                return BadRequest(new { errorField = "description-image", message = "Failed to upload, could not find associated recipe" });
            }
            if (recipe.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
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

        [Authorize]
        [HttpPost]
        [Route("/api/Recipe/AddInstructionImage")]
        public async Task<IActionResult> UploadInstrImage(Instruction_Image img)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            if (string.IsNullOrEmpty(img.ImageBase64) || string.IsNullOrEmpty(img.FileExtension))
            {
                return BadRequest(new { errorField = "instruction-image", message = "cannot be nothing", index = img.Instruction?.Sequence_Number });
            }
            
            var instruction = await _recipeService.GetInstructionByUUID(img.InstructionUUID);
            img.Instruction = instruction;
            if (instruction == null)
            {
                return BadRequest(new { errorField = "instruction-image", message = "Failed to upload, could not find associated instruction", index = "?"});
            }
            
            var imageError = ImageValidator.validateInput(img.ImageBase64, img.FileExtension);
            if (imageError != null)
            {
                return BadRequest(new {imageError, index=img.Instruction?.Sequence_Number});
            }

            var recipe = await _recipeService.GetRecipeByUUID(instruction.RecipeUUID);
            if (recipe.OwnerUUID != ownerUUID)
            {
                return Unauthorized();
            }

            img.Instruction = null;
            var result = await _recipeService.UploadInstructionImage(img);
            if (string.IsNullOrEmpty(result.Url))
            {
                return BadRequest(new {errorField = "description_image", message="Failed to upload", index=img.Instruction?.Sequence_Number});
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/Recipe/User")]
        public async Task<IActionResult> GetUserRecipes()
        {
            string token = Request.Headers["Authorization"];
            var userUUID = await _userService.GetUUIDFromToken(token);
            if (userUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            var userRecipes = await _recipeService.GetUserRecipes(userUUID);
            if (userRecipes == null)
            {
                return NotFound(new {Error="No recipes found for this user" });
            }
            return Ok(userRecipes);
        }


        [Authorize]
        [HttpGet("{UUID}")]
        public async Task<IActionResult> GetRecipe(Guid UUID)
        {
            string token = Request.Headers["Authorization"];
            var requestorUUID = await _userService.GetUUIDFromToken(token);
            if (requestorUUID == Guid.Empty)
            {
                return Unauthorized(new { Error = "Invalid session." });
            }
            var requestedRecipe = await _recipeService.GetRecipeByUUIDIncludeAll(UUID);
            if (requestedRecipe == null)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }
            if (requestorUUID!=requestedRecipe.OwnerUUID && requestedRecipe.IsViewableByPublic==false)
            {
                return Unauthorized(new { Error = "You do not have permission to access this resource" });
            }
            bool hasPermission = false;
            if (requestorUUID == requestedRecipe.OwnerUUID)
            {
                hasPermission = true;
            }

            var recipeDMedia = await _recipeService.GetDescriptionMedia(UUID);

            requestedRecipe.OwnerUUID = Guid.Empty;

            return Ok(new {recipe=requestedRecipe, recipe_description_media=recipeDMedia, has_edit_permission=hasPermission});
        }

        
        [HttpGet("/api/Recipe/NoAuth/{UUID}")]
        public async Task<IActionResult> GetPublicRecipe(Guid UUID)
        {
            var requestedRecipe = await _recipeService.GetRecipeByUUIDIncludeAll(UUID);
            if (requestedRecipe == null)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }
            if (requestedRecipe.IsViewableByPublic == false)
            {
                return Unauthorized(new { Error = "You need to be signed in to access this resource" });
            }

            var recipeDMedia = await _recipeService.GetDescriptionMedia(UUID);
            requestedRecipe.OwnerUUID = Guid.Empty;

            return Ok(new { recipe = requestedRecipe, recipe_description_media = recipeDMedia });
        }

        //[EnableCors]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe newRecipe)
        {
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.GetUUIDFromToken(token);
            if(ownerUUID == Guid.Empty)
            {
                return NotFound(new {Error = "User not found. Try Relogging." });
            }
            var validationError = RecipeValidator.validateInput(newRecipe);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }
            newRecipe.OwnerUUID = ownerUUID;
            var recipe = await _recipeService.CreateRecipe(newRecipe);
            return Ok(recipe);
        }

        //[HttpPost]
        //[Route("/api/Recipe/validate")]
        //public async Task<IActionResult> ValidateRecipe(Recipe newRecipe)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route("/api/Recipe/diets")]
        //public async Task<IActionResult> GetDiets()
        //{
        //    var diets = await _recipeService.GetDiets();
        //    if(diets == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(diets);
        //    }
        //}

        //[HttpGet]
        //[Route("/api/Recipe/cuisines")]
        //public async Task<IActionResult> GetCuisines()
        //{
        //    var cuisines = await _recipeService.GetCuisines();
        //    if(cuisines == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(cuisines);
        //    }
        //}

        //[HttpGet]
        //[Route("/api/Recipe/tags")]
        //public async Task<IActionResult> GetTags()
        //{
        //    var tags = await _recipeService.GetTags();
        //    if (tags == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(tags);
        //    }
        //}

        [Authorize]
        [HttpDelete]
        [Route("/api/Recipe/UnusedImages/Delete")]
        public async Task<IActionResult> RemoveUnusedImages(List<string> urlList)
        {
            //var urlList = Urls.UrlList;
            var result = await _recipeService.removeUnusedImages(urlList);
            if (result == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
