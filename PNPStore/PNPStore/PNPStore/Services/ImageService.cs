namespace PNPStore.Services
{
    public class ImageService
    {
        public ImageService()
        {

        }
        public async Task<bool> UploadImage(IFormFile? file, string imageName)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName+".png");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading image: " + ex.Message);
            }
        }
    }
}
