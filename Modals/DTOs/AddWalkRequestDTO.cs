using System.ComponentModel.DataAnnotations;

namespace ExploreAPIs.API.Modals.DTOs
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name has to be a minimum of 1 character.")]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 3 characters.")]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(0,999999)]
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
