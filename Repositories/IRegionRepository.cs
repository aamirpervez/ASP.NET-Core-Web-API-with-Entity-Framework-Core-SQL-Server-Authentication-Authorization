using ExploreAPIs.API.Modals.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ExploreAPIs.API.Repositories
{
    public interface IRegionRepository
    {
         Task<List<Region>> GetAllAsync();

        Task<List<Region>> GetById(Guid? id,string? regionIDList);

        Task<Region> CreateAsyc(Region region);

        Task<Region?> UpdateAsyc(Region region);

        Task<bool> DeleteByIdAsync(Guid ID);
    }
}
