using AutoMapper;
using ExploreAPIs.API.CustomActionFilters;
using ExploreAPIs.API.Data;
using ExploreAPIs.API.Modals.Domain;
using ExploreAPIs.API.Modals.DTOs;
using ExploreAPIs.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ExploreAPIs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ExploreAPIsDbContext _exploreAPIsDbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(ExploreAPIsDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this._exploreAPIsDbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

       // [HttpGet]
       //// [Authorize(Roles ="Reader")]
       // public async Task<IActionResult> GetAll()
       // {
       //       //  throw new Exception("This is custom Exception...");
       //         //logger.LogInformation("Get All Regions Action Method Invoked!");
       //         //logger.LogWarning("This is Regions Warning Log at GetAll Action Invoked");
       //         //logger.LogWarning("This is Regions Error Log at GetAll Action Invoked");

       //         //Get Data from Database into DomainModels..
       //         var regionDomaains = await regionRepository.GetAllAsync();

       //         //Map Domain Modals to DTOs, using AutoMapper.
       //         var regionsDto = mapper.Map<List<RegionDTOs>>(regionDomaains);

       //         logger.LogInformation($"API Result : {JsonSerializer.Serialize(regionsDto)}");

       //         //RETUN DTOS
       //         return Ok(regionsDto);
           
       // }

        [HttpGet]
         [Authorize(Roles = "Reader")]
       // [Route("{id:Guid?}")]
       // [HttpGet("GetById/{id:Guid?}/{regionIDList}")]
        public async Task<IActionResult> GetById([FromQuery] Guid? id = null, [FromQuery]  string? regionIDList = null)
        {
          //  throw new Exception("This is custom Exception...");

            var regionDomaains = await regionRepository.GetById(id, regionIDList);

            if (regionDomaains == null)
            {
                return NotFound();
            }

            var regionsDto = mapper.Map<List<RegionDTOs>>(regionDomaains);

            return Ok(regionsDto);
        }

        [HttpPost]
      //  [Authorize(Roles = "Writer")]
        [ValidateModal]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
                //Map DTOs to Domain Modal, using AutoMapper
                var regionDomainModal = mapper.Map<Region>(addRegionRequestDTO);

                //Use region domain modal to create region in DB...
                regionDomainModal = await regionRepository.CreateAsyc(regionDomainModal);

                //Map Domain Modal to DTO, using Auto Mapper..
                var regionDTO = mapper.Map<RegionDTOs>(regionDomainModal);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
      //  [Authorize(Roles = "Writer")]
        //[Route("{id:Guid}")]
      //  [ValidateModal]
        public async Task<IActionResult> Update([FromBody] RegionDTOs addRegionRequestDTO)
        {
                //Map DTO to Domian Model, using Automapper..
                var regionDomainModal = mapper.Map<Region>(addRegionRequestDTO);

                //Check if region exists, so update it.
                regionDomainModal = await regionRepository.UpdateAsyc(regionDomainModal);

                if (regionDomainModal == null)
                {
                    return NotFound();
                }

                //Map Domain Modal to DTO, using Auto Mapper..
                var regionDTO = mapper.Map<RegionDTOs>(regionDomainModal);

                return Ok(regionDTO);
        }

        [HttpDelete]
    //    [Authorize(Roles = "Reader,Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid ID)
        {
            // Call the repository to delete the region
            var isDeleted = await regionRepository.DeleteByIdAsync(ID);

            if (isDeleted)
            {
                // If deletion was successful, return NoContent (204)
                return NoContent();
            }
            else
            {
                // If no region was found, return NotFound (404)
                return NotFound();
            }
        }
    }
}
