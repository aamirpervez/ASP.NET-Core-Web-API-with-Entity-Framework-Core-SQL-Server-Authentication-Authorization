using System.ComponentModel.DataAnnotations;

namespace ExploreAPIs.API.Modals.DTOs
{
    public class ImageUploadRequestDTOs
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

    }
}
