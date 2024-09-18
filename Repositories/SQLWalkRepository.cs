using ExploreAPIs.API.Data;
using ExploreAPIs.API.Modals.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ExploreAPIs.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ExploreAPIsDbContext exploreAPIsDbContext;

        public SQLWalkRepository(ExploreAPIsDbContext exploreAPIsDbContext)
        {
            this.exploreAPIsDbContext = exploreAPIsDbContext;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string ? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var walks = exploreAPIsDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Apply Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Apply Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }


            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsyc(Guid id)
        {
          return   await exploreAPIsDbContext.Walks.
                Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await exploreAPIsDbContext.Walks.AddAsync(walk);
            await exploreAPIsDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
           var existingWalk = await exploreAPIsDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImgUrl = walk.WalkImgUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await exploreAPIsDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await exploreAPIsDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

             exploreAPIsDbContext.Walks.Remove(existingWalk);
            await exploreAPIsDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
