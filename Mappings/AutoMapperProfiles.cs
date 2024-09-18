using AutoMapper;
using ExploreAPIs.API.Modals.Domain;
using ExploreAPIs.API.Modals.DTOs;

namespace ExploreAPIs.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTOs>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTOs>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTOs>().ReverseMap();

        }
    }
}
