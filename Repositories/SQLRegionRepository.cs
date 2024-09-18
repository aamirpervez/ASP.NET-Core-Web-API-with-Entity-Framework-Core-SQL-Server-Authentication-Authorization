using Dapper;
using ExploreAPIs.API.Data;
using ExploreAPIs.API.Modals.Domain;
using ExploreAPIs.API.Modals.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static ExploreAPIs.API.Repositories.SQLRegionRepository;

namespace ExploreAPIs.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ExploreAPIsDbContext _exploreAPIsDbContext;

        public SQLRegionRepository(ExploreAPIsDbContext dbContext)
        {
            this._exploreAPIsDbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            try
            {

                // Use Dapper within the repository class to execute the stored procedure
                using (var connection = _exploreAPIsDbContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    // Fetch JSON result using Dapper
                    var jsonResult = await connection.QuerySingleAsync<string>("EXEC dbo.APIv1_REGIONS_Select");

                   // return jsonResult;

                    return JsonConvert.DeserializeObject<List<Region>>(jsonResult);
                }


                // await Regions.FromSqlRaw("EXEC dbo.MyStoredProcedureName").ToListAsync();

                //return await _exploreAPIsDbContext.Regions.ToListAsync();

                // return await _exploreAPIsDbContext.Regions.FromSqlRaw("EXEC dbo.APIv1_REGIONS_Select").ToListAsync();


                //var jsonResult = await _exploreAPIsDbContext.Regions.FromSqlRaw("EXEC dbo.APIv1_REGIONS_Select").ToListAsync();

                //    var jsonResult = await _exploreAPIsDbContext.Regions
                //.FromSqlRaw<string>("EXEC GetProductsAsJson")
                //.AsAsyncEnumerable().ToString();


                // Deserialize the JSON into a list of Region objects
           //     return JsonConvert.DeserializeObject<List<Region>>(jsonResult.ToString());

                // Assuming the stored procedure returns a single column with JSON as a result
                //var jsonString = await JsonResults.FromSqlRaw("EXEC dbo.APIv1_REGIONS_Select")
                //    .Select(j => j.JsonColumn) // Ensure the column name matches your database output
                //    .FirstOrDefaultAsync();

                //// Deserialize the JSON string into a list of Region objects
                //if (!string.IsNullOrEmpty(jsonString))
                //{
                //    return JsonSerializer.Deserialize<List<Region>>(jsonString);
                //}

                //return new List<Region>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<Region>> GetById(Guid? id = null, string? regionIDList = null)
        {
            try
            {
                // Use Dapper within the repository class to execute the stored procedure
                using (var connection = _exploreAPIsDbContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    // Use Dapper's anonymous object to pass parameters
                    var parameters = new { RegionID = id, RegionIDList = regionIDList };

                    var jsonResult = await connection.QuerySingleAsync<string>(
                    "EXEC dbo.APIv1_REGIONS_Select @RegionID, @RegionIDList", parameters);

                    return JsonConvert.DeserializeObject<List<Region>>(jsonResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Region> CreateAsyc(Region region)
        {
            try
            {

                using (var connection = _exploreAPIsDbContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    // Serialize the object to JSON
                    string productJson = JsonConvert.SerializeObject(region);

                    // Pass the JSON string to the stored procedure
                    var parameters = new { JSONrequest = productJson };

                    var jsonResult = await connection.QuerySingleOrDefaultAsync<string>("EXEC APIv1_REGIONS_Upsert  @JSONrequest", parameters);

                    if (jsonResult == null) 
                    {
                        // Handle the case where the stored procedure didn't return a result
                        return null; // Or throw an exception or handle as needed
                    }

                    // Deserialize the JSON array into a list of RegionResponseDto objects
                    List<Region> regions = JsonConvert.DeserializeObject<List<Region>>(jsonResult);

                    // Get the first element from the list (0th index)
                    Region regionResponse = regions.FirstOrDefault();

                    return regionResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
               // throw ;
            }
          


            return region;

        }

        public async Task<Region?> UpdateAsyc(Region region)
        {
            try
                {
                using (var connection = _exploreAPIsDbContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    // Serialize the object to JSON
                    string productJson = JsonConvert.SerializeObject(region);

                    // Pass the JSON string to the stored procedure
                    var parameters = new { JSONrequest = productJson };

                    var jsonResult = await connection.QuerySingleOrDefaultAsync<string>("EXEC APIv1_REGIONS_Upsert  @JSONrequest", parameters);

                    if (jsonResult == null)
                    {
                        // Handle the case where the stored procedure didn't return a result
                        return null; // Or throw an exception or handle as needed
                    }

                    // Deserialize the JSON array into a list of RegionResponseDto objects
                    List<Region> regions = JsonConvert.DeserializeObject<List<Region>>(jsonResult);

                    // Get the first element from the list (0th index)
                    Region regionResponse = regions.FirstOrDefault();

                    return regionResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                // throw ;
            }



            return region;
        }

        public async Task<bool> DeleteByIdAsync(Guid ID)
        {
            using (var connection = _exploreAPIsDbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                var parameters = new { RegionID = ID.ToString() };

                // Execute the stored procedure. No result is expected.
                int rowsAffected = await connection.ExecuteAsync("EXEC APIv1_REGIONS_Delete @RegionID", parameters);

                // Return true if at least one row was deleted, otherwise return false.
                return rowsAffected > 0;
            }
        }
    }
}
