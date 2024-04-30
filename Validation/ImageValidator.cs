using System.Drawing;

namespace RecipeSiteBackend.Validation
{
    public class ImageValidator
    {
        public static ValidationError? validateInput(string imgBase64, string fileExtension)
        {
            string[] validFileExtensions = [".tif", ".tiff", ".bmp", ".jpg", ".jpeg", ".gif", ".png"];
            int match = 0;
            foreach (string ext in validFileExtensions)
            {
                if (match > 0)
                {
                    break;
                }
                if(fileExtension.ToLower() ==  ext)
                {
                    match++;
                }
            }
            if (match <= 0) 
            {
                return new ValidationError()
                {
                    ErrorField = "description_image",
                    Message = "Invalid file extension"
                };
            }

           

            var base64length = imgBase64.AsSpan().Slice(imgBase64.IndexOf(',') + 1).Length;
            var fileSizeInByte = Math.Ceiling((double)base64length / 4) * 3;

            if(fileSizeInByte > 2300000)  //Max. allowed 2MB = 2097152B. Set to 2300000 to allow for some leeway in inaccurate size calculation
            {
                return new ValidationError()
                {
                    ErrorField = "description_image",
                    Message = "Max. size 2MB allowed"
                };
            }

            byte[] imgBytes = Convert.FromBase64String(imgBase64);
            Image image;
            using (var ms = new MemoryStream(imgBytes, 0, imgBytes.Length))
            {
                try
                {
                    image = Image.FromStream(ms, true); //attempt to convert to image to confirm that it is indeed an image.
                }
                catch (Exception ex)
                {
                    Console.WriteLine("byte[] to image error: " + ex.Message);
                    return new ValidationError()
                    {
                        ErrorField = "description_image",
                        Message = "Not a valid image"
                    };
                }
            }
            return null;

        }
    }
}
