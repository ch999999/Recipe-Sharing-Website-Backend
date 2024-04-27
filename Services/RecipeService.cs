using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
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

        public async Task<List<Ingredient>> GetRecipeIngredients(Guid recipeUUID)
        {
            return await _context.Ingredients.AsNoTracking().Where(i => i.RecipeUUID == recipeUUID).ToListAsync();
        }

        public async Task<List<Note>> GetRecipeNotes(Guid recipeUUID)
        {
            return await _context.Notes.AsNoTracking().Where(n => n.RecipeUUID == recipeUUID).ToListAsync();
        }

        public async Task<List<Instruction>> GetRecipeInstructions(Guid recipeUUID)
        {
            return await _context.Instructions.AsNoTracking().Where(i => i.RecipeUUID == recipeUUID).ToListAsync();
        }

        public async Task<List<Instruction_Image>> GetRecipeInstructionImages(Guid recipeUUID)
        {
            return await _context.Instruction_Images.AsNoTracking().Where(im => im.Instruction.RecipeUUID == recipeUUID).ToListAsync();
        }

        public async Task<string> DeleteRecipe(Recipe recipe)
        {
            try
            {
                List<string> awsToDelete = [];

                var recipeInstructionImages = await GetRecipeInstructionImages(recipe.UUID);
                if (recipeInstructionImages.Count > 0)
                {
                    foreach (var image in recipeInstructionImages)
                    {
                        var url = image.Url;
                        if (string.IsNullOrEmpty(url))
                        {
                            continue;
                        }
                        awsToDelete.Add(url);
                    }
                }
                var descriptionImage = await GetDescriptionMedia(recipe.UUID);
                if (descriptionImage != null)
                {
                    var url = descriptionImage.Url;
                    if (!string.IsNullOrEmpty(url))
                    {
                        awsToDelete.Add(url);
                    }
                }

                _context.Remove(recipe);
                await _context.SaveChangesAsync();
                AmazonS3Client client = new AmazonS3Client(_configuration["AWS:ACCESS_KEY"], _configuration["AWS:SECRET_KEY"], Amazon.RegionEndpoint.APSoutheast1);
                using (client)
                {
                    if (awsToDelete.Count > 0)
                    {
                        foreach (var url in awsToDelete)
                        {
                            var key = url.Substring(url.LastIndexOf("/") + 1);
                            await client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest() { BucketName = _configuration["AWS:AWS_BUCKET"], Key = key });
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Recipe>? UpdateRecipe(Recipe recipe)
        {
            try
            {
                var recipeToUpdate = await _context.Recipes.FindAsync(recipe.UUID);
                if (recipeToUpdate == null)
                {
                    return null;
                }
                recipeToUpdate.Title = recipe.Title;
                recipeToUpdate.Description = recipe.Description;
                recipeToUpdate.Prep_Time_Mins = recipe.Prep_Time_Mins;
                recipeToUpdate.Cook_Time_Mins = recipe.Cook_Time_Mins;
                recipeToUpdate.Servings = recipe.Servings;
                recipeToUpdate.LastModifiedDate = DateTime.Now.ToUniversalTime();
                recipeToUpdate.IsViewableByPublic = false;

                List<Ingredient> ingredients = await GetRecipeIngredients(recipe.UUID);
                _context.Ingredients.RemoveRange(ingredients);
                recipeToUpdate.Ingredients = recipe.Ingredients;

                List<Note> notes = await GetRecipeNotes(recipe.UUID);
                if (notes != null && notes.Count > 0)
                {
                    _context.Notes.RemoveRange(notes);
                }
                recipeToUpdate.Notes = recipe.Notes;

                List<string> awsToDelete = [];
                List<Instruction_Image> instruction_images = await GetRecipeInstructionImages(recipe.UUID);
                if (instruction_images.Count > 0)
                {
                    foreach (var image in instruction_images)
                    {
                        if (!string.IsNullOrEmpty(image.Url))
                        {
                            awsToDelete.Add(image.Url);
                        }
                    }
                }
                _context.Instruction_Images.RemoveRange(instruction_images);

                List<Instruction> instructions = await GetRecipeInstructions(recipe.UUID);
                _context.Instructions.RemoveRange(instructions);
                recipeToUpdate.Instructions = recipe.Instructions;

                await _context.SaveChangesAsync();
                
                return recipeToUpdate;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Recipe>? GetRecipeByUUID(Guid uuid) //formerly GetByUUID
        {
            try
            {
                return await _context.Recipes.AsNoTracking().SingleOrDefaultAsync(r => r.UUID == uuid);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Recipe>? GetRecipeByUUIDIncludeAll(Guid uuid) //formerly GetByUUIDIncludeAll
        {
            try
            {
                return await _context.Recipes
                    .Include(r => r.Ingredients)
                    .Include(r => r.Policies)
                    .Include(r => r.Notes)
                    .Include(r => r.Instructions)
                    .ThenInclude(instr => instr.Images)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.UUID == uuid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<Instruction>? GetInstructionByUUID(Guid uuid)
        {
            return await _context.Instructions.AsNoTracking().SingleOrDefaultAsync(i => i.UUID == uuid);
        }

        public async Task<Description_Media>? GetDescriptionMedia(Guid uuid) //formerly getDescription_Media
        {
            return await _context.Description_Medias.AsNoTracking().SingleOrDefaultAsync(dm => dm.RecipeUUID == uuid);
        }

        public async Task<Description_Media> TransferDescriptionMedia(Description_Media media)
        {
            var existing = await _context.Description_Medias.AsNoTracking().SingleOrDefaultAsync(dm => dm.RecipeUUID == media.RecipeUUID);
            if (existing != null)
            {
                _context.Description_Medias.Remove(existing);
            }
            await _context.Description_Medias.AddAsync(media);
            await _context.SaveChangesAsync();
            return media;
        }

        public async Task<Description_Media> UploadDescriptionMedia(Description_Media media)
        {
            var imgBase64 = media.ImageBase64;

            byte[] imgBytes = Convert.FromBase64String(imgBase64);
            var randomFileName = Guid.NewGuid().ToString();
            var imgName = randomFileName + media.FileExtension;
            var imgPath = "./Temp/" + imgName;
            File.WriteAllBytes(imgPath, imgBytes);

            try
            {
                BasicAWSCredentials creds = new BasicAWSCredentials(_configuration["AWS:ACCESS_KEY"], _configuration["AWS:SECRET_KEY"]);
                var fileTransferUtility = new TransferUtility(new AmazonS3Client(creds, Amazon.RegionEndpoint.APSoutheast1));
                var filePath = imgPath;
                var bucketName = _configuration["AWS:AWS_BUCKET"];
                var keyName = imgName;
                fileTransferUtility.Upload(filePath, bucketName, keyName);
                var imgUrl = "https://" + bucketName + ".s3." + _configuration["AWS:AWS_REGION"] + ".amazonaws.com/" + keyName;

                media.Url = imgUrl;

                await _context.Description_Medias.AddAsync(media);
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine("S3 Error: " + ex.Message);
            }
            finally
            {
                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
            }
            media.ImageBase64 = null;
            return media;
        }

        public async Task<Instruction_Image> FindImageByUrl(string url) //formerly FindImageByUrl
        {
            return await _context.Instruction_Images.AsNoTracking().FirstOrDefaultAsync(im => im.Url == url);
        }

        public async Task<Instruction_Image> UploadInstructionImage(Instruction_Image image)
        {
            var imgBase64 = image.ImageBase64;

            byte[] imgBytes = Convert.FromBase64String(imgBase64);
            var randomFileName = Guid.NewGuid().ToString();
            var imgName = randomFileName + image.FileExtension;
            var imgPath = "./Temp/" + imgName;
            File.WriteAllBytes(imgPath, imgBytes);
            string imgUrl = string.Empty;

            try
            {
                BasicAWSCredentials creds = new BasicAWSCredentials(_configuration["AWS:ACCESS_KEY"], _configuration["AWS:SECRET_KEY"]);
                var fileTransferUtility = new TransferUtility(new AmazonS3Client(creds, Amazon.RegionEndpoint.APSoutheast1));
                var filePath = imgPath;
                var bucketName = _configuration["AWS:AWS_BUCKET"];
                var keyName = imgName;
                fileTransferUtility.Upload(filePath, bucketName, keyName);
                imgUrl = "https://" + bucketName + ".s3." + _configuration["AWS:AWS_REGION"] + ".amazonaws.com/" + keyName;
                image.Url = imgUrl;
                image.Image_Number = 1;

                await _context.Instruction_Images.AddAsync(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("S3 Error: " + ex.Message);
            }
            finally
            {
                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
            }
            image.ImageBase64 = null;
            return image;
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipes(Guid ownerUUID)
        {
            var userRecipes = await _context.Recipes.Where(r => r.OwnerUUID == ownerUUID).ToListAsync();
            try
            {
                foreach (Recipe recipe in userRecipes)
                {
                    recipe.OwnerUUID = Guid.Empty;
                }
            }
            catch
            {
                return null;
            }
            return userRecipes;
        }

        public async Task<Description_Media> FindDescriptionMedia(Guid recipeUUID)
        {
            var media = await _context.Description_Medias.SingleOrDefaultAsync(m => m.RecipeUUID == recipeUUID);
            return media;
        }

        public async void DeleteDescriptionMedia(Description_Media media)
        {
            _context.Description_Medias.Remove(media);
            _context.SaveChanges();
            
        }

        public async Task<Description_Media> FindDescriptionMediaByUrl(string url)
        {
            return await _context.Description_Medias.AsNoTracking().FirstOrDefaultAsync(m => m.Url == url);
        }

        public async Task<Description_Media> UpdateDescriptionMediaByUrl(Description_Media img)
        {
            var media = await _context.Description_Medias.SingleOrDefaultAsync(dm => dm.RecipeUUID == img.RecipeUUID);
            if (media == null)
            {
                await _context.Description_Medias.AddAsync(img);
                await _context.SaveChangesAsync();
                return img;
            }
            else
            {
                try
                {
                    media.Url = img.Url;

                    media.Filename = img.Filename;
                    await _context.SaveChangesAsync();

                    return media;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return media;
                }
            }
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            recipe.UUID = Guid.Empty;
            recipe.Owner = null;
            var ownerPolicy = new Permitted_User()
            {
                Permission_Level = "owner",
                UserUUID = recipe.OwnerUUID
            };
            recipe.Policies = [];
            recipe.Policies.Add(ownerPolicy);
            recipe.CreatedDate = DateTime.Now.ToUniversalTime();
            recipe.LastModifiedDate = DateTime.Now.ToUniversalTime();

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return new Recipe
            {
                Title = recipe.Title,
                UUID = recipe.UUID,
                Instructions = recipe.Instructions
            };
        }

        public bool checkIfImageUsed(string url)
        {

            var a = _context.Description_Medias.AsNoTracking().FirstOrDefault(dm => dm.Url == url);
            var b = _context.Instruction_Images.AsNoTracking().FirstOrDefault(im => im.Url == url);
            //if (a != null || b != null)
            //{
            //    return true;
            //}
            //return false;
            if (a == null && b == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> removeUnusedImages(List<string> urls)
        {
            
            List<string> awsToDelete = [];
            if (urls.Count > 0)
            {
                foreach (string url in urls)
                {
                    bool imageUsed = checkIfImageUsed(url);
                    if (!imageUsed)
                    {
                        
                        awsToDelete.Add(url);
                    }
                }
            }

            if (awsToDelete.Count > 0)
            {
                try
                {
                    AmazonS3Client client = new AmazonS3Client(_configuration["AWS:ACCESS_KEY"], _configuration["AWS:SECRET_KEY"], Amazon.RegionEndpoint.APSoutheast1);
                    using (client)
                    {

                        foreach (var url in awsToDelete)
                        {
                            var key = url.Substring(url.LastIndexOf("/") + 1);
                            await client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest() { BucketName = _configuration["AWS:AWS_BUCKET"], Key = key });
                        }

                    }
                    return null;
                }
                catch
                {
                    return "Errors";
                }
            }
            return null;
        }

        
    }
}
