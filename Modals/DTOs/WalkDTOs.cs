using ExploreAPIs.API.Modals.Domain;

namespace ExploreAPIs.API.Modals.DTOs
{
    public class WalkDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
