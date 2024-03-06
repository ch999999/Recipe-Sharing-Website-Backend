﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpPut]
        [Route("/api/Recipe/UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe(Recipe newRecipe)
        {
            
            string token = Request.Headers["Authorization"];
            var ownerUUID = await _userService.getUUIDFromToken(token);
            if (ownerUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            var recipeToUpdate = await _recipeService.GetByUUID(newRecipe.UUID);
            if(recipeToUpdate == null)
            {
                return BadRequest();
            }
            if (recipeToUpdate.OwnerUUID != ownerUUID)
            {
                return NotFound();
            }

            foreach(Instruction instruction in newRecipe.Instructions)
            {
                if (instruction.Images.Count > 0)
                {
                    if(instruction.Images.Count > 1)
                    {
                        return BadRequest("Invalid operation. Only one image allowed per instruction"); //change instruction - instruction-images to be one-one instead of one-may later 
                    }
                    var images = instruction.Images.ToList();
                    var urlExistsInInstructions = await _recipeService.FindImageByUrl(images[0].Url);
                    var urlExistsInDescription = await _recipeService.FindDescriptionMediaByUrl(images[0].Url);
                    if(urlExistsInInstructions == null && urlExistsInDescription==null)
                    {
                        return BadRequest(new {error = "Invalid Url", problemChild = images[0].Url });
                    }
                }
            }
            var updatedRecipe = await _recipeService.UpdateRecipe(newRecipe);
            return Ok(updatedRecipe);
        }

        [Authorize]
        [HttpDelete("/api/Recipe/DescriptionImage/{UUID}")]
        public async Task<IActionResult> DeleteDescpImage(Guid UUID)
        {
            Description_Media mediaToDelete = await _recipeService.getDescription_Media(UUID);
            if (mediaToDelete != null)
            {
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
            if (imageError != null)
            {
                return BadRequest(imageError);
            }

            var mediaToDelete = await _recipeService.getDescription_Media(img.RecipeUUID);
            if(mediaToDelete != null)
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
            if (string.IsNullOrEmpty(img.Url))
            {
                return BadRequest(new { ErrorField = "description_image", message = "cannot be nothing" });
            }
            var recipe = await _recipeService.GetByUUID(img.RecipeUUID);
            if (recipe == null)
            {
                return BadRequest(new { errorField = "description-image", message = "Failed to upload, could not find associated recipe" });
            }
            var existsInInstructions = await _recipeService.FindImageByUrl(img.Url);
            var existsInDescriptionMedia = await _recipeService.FindDescriptionMediaByUrl(img.Url);
            if(existsInDescriptionMedia == null && existsInInstructions == null)
            {
                return BadRequest(new {error= "Invalid Url" });
            }

            var result = await _recipeService.UpdateDescriptionMediaByUrl(img);
            return Ok(result);
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
            if (string.IsNullOrEmpty(img.ImageBase64) || string.IsNullOrEmpty(img.FileExtension))
            {
                return BadRequest(new { errorField = "instruction-image", message = "cannot be nothing", index = img.Instruction?.Sequence_Number });
            }
            Console.WriteLine(img.ToString());
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
            var userUUID = await _userService.getUUIDFromToken(token);
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
            var requestorUUID = await _userService.getUUIDFromToken(token);
            if (requestorUUID == Guid.Empty)
            {
                return NotFound(new { Error = "User not found. Try Relogging." });
            }
            var requestedRecipe = await _recipeService.GetByUUIDIncludeAll(UUID);
            if (requestedRecipe == null)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }
            if (requestorUUID!=requestedRecipe.OwnerUUID && requestedRecipe.IsViewableByPublic==false)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }
            bool hasPermission = false;
            if (requestorUUID == requestedRecipe.OwnerUUID)
            {
                hasPermission = true;
            }

            var recipeDMedia = await _recipeService.getDescription_Media(UUID);

            requestedRecipe.OwnerUUID = Guid.Empty;

            return Ok(new {recipe=requestedRecipe, recipe_description_media=recipeDMedia, has_edit_permission=hasPermission});
        }

        
        [HttpGet("/api/Recipe/NoAuth/{UUID}")]
        public async Task<IActionResult> GetPublicRecipe(Guid UUID)
        {
            var requestedRecipe = await _recipeService.GetByUUIDIncludeAll(UUID);
            if (requestedRecipe == null)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }
            if (requestedRecipe.IsViewableByPublic == false)
            {
                return NotFound(new { Error = "Could not locate requested resource" });
            }

            var recipeDMedia = await _recipeService.getDescription_Media(UUID);
            requestedRecipe.OwnerUUID = Guid.Empty;

            return Ok(new { recipe = requestedRecipe, recipe_description_media = recipeDMedia });
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