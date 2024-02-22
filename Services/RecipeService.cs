using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RecipeSiteBackend.Data;
using RecipeSiteBackend.Models;
using System.Drawing;
using System.IO;


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

        public async Task<Recipe>? GetByUUID(Guid uuid)
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

        public async Task<Description_Media> UploadDescriptionMedia(Description_Media media)
        {
            var imgBase64 = media.ImageBase64;

            byte[] imgBytes = Convert.FromBase64String(imgBase64);
            var randomFileName = Guid.NewGuid().ToString();
            var imgName = randomFileName + media.FileExtension;
            var imgPath = "./Temp/" + imgName;
            File.WriteAllBytes(imgPath, imgBytes);
            string imgUrl = string.Empty;
            
            try
            {
                BasicAWSCredentials creds = new BasicAWSCredentials(_configuration["AWS:ACCESS_KEY"], _configuration["AWS:SECRET_KEY"]);
                var fileTransferUtility = new TransferUtility(new AmazonS3Client(creds,Amazon.RegionEndpoint.APSoutheast1));
                var filePath = imgPath;
                var bucketName = _configuration["AWS:AWS_BUCKET"];
                var keyName = imgName;
                fileTransferUtility.Upload(filePath, bucketName, keyName);
                imgUrl = "https://" + bucketName + ".s3." + _configuration["AWS:AWS_REGION"] + ".amazonaws.com/" + keyName;

                media.Url = imgUrl;
                await _context.Description_Medias.AddAsync(media);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                Console.WriteLine("S3 Error: "+ex.Message);
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

        public async Task<IEnumerable<Difficulty>> GetDifficulties()
        {
            return await _context.Difficulties.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Cuisine>> GetCuisines()
        {
            return await _context.Cuisines.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Diet>> GetDiets()
        {
            return await _context.Diets.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _context.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            recipe.UUID = Guid.Empty;
            recipe.Owner = null;
            recipe.Ratings = null;
            recipe.Cuisine = null;
            recipe.Meal_Type = null;
            recipe.Source = null;
            recipe.Difficulty = null;
            var ownerPolicy = new Permitted_User()
            {
                Permission_Level = "owner",
                UserUUID = recipe.OwnerUUID
            };
            recipe.Policies = [];
            recipe.Policies.Add(ownerPolicy);
            recipe.CreatedDate = DateTime.Now.ToUniversalTime();
            recipe.LastModifiedDate = DateTime.Now.ToUniversalTime();

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
                UUID = recipe.UUID
            };
        }

        
    }
}
