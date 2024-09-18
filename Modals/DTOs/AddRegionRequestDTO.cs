using System.ComponentModel.DataAnnotations;

namespace ExploreAPIs.API.Modals.DTOs
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(1,ErrorMessage = "Code has to be a minimum of 1 characters.")]
        [MaxLength(10, ErrorMessage = "Code has to be a maximum of 10 characters.")]
        public string Code { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name has to be a minimum of 1 character.")]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
