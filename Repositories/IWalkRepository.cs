using ExploreAPIs.API.Modals.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ExploreAPIs.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery=null, string? sortBy=null, bool isAscending = true, int pageNumber = 1,  int pageSize = 10);

        Task<Walk> CreateAsync(Walk walk);

        Task<Walk?> GetByIdAsyc(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
