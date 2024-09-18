using ExploreAPIs.API.Data;
using ExploreAPIs.API.Modals.Domain;

namespace ExploreAPIs.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ExploreAPIsDbContext exploreAPIsDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ExploreAPIsDbContext exploreAPIsDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.exploreAPIsDbContext = exploreAPIsDbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
                $"{ image.FileName}");

            //Upload Image/PDF to Local Path..
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhos:1234/images/image1.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}";
            image.FilePath = urlFilePath;

            //Save changes to DB - Add Image to Images Folder..
            await exploreAPIsDbContext.Images.AddAsync(image);
            await exploreAPIsDbContext.SaveChangesAsync();

            return image;
        }
    }
}
