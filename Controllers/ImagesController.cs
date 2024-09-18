using ExploreAPIs.API.Modals.Domain;
using ExploreAPIs.API.Modals.DTOs;
using ExploreAPIs.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace ExploreAPIs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        //POST : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTOs requestDTOs)
        {
            ValidateFileUpload(requestDTOs);

            if (ModelState.IsValid)
            {
                //Convert Dto Modal to Domain Model

                var imageDomainModal = new Image
                {
                    File = requestDTOs.File,
                    FileExtension = Path.GetExtension(requestDTOs.File.FileName),
                    FileSizeInBytes = requestDTOs.File.Length,
                    FileName = requestDTOs.File.FileName,
                    FileDescription = requestDTOs.FileDescription
                };
                

                //Use repository to upload image..
                await imageRepository.Upload(imageDomainModal); 

                return Ok(imageDomainModal);
            }

            return BadRequest(ModelState);
        }

        private  void ValidateFileUpload(ImageUploadRequestDTOs requestDTOs)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };

            if(!allowedExtensions.Contains(Path.GetExtension(requestDTOs.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }

            if (requestDTOs.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload smaller size file.");
            }
        }
    }
}
