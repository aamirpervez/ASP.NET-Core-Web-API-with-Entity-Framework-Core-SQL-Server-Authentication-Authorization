using AutoMapper;
using ExploreAPIs.API.CustomActionFilters;
using ExploreAPIs.API.Modals.Domain;
using ExploreAPIs.API.Modals.DTOs;
using ExploreAPIs.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExploreAPIs.API.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        //api/walk?filterOn=Name&filterQuery=Track$sortBy=Name&isAscending=True
        public async Task<ActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize= 10)
        {
            var walksDomainModal = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy,isAscending ?? true, pageNumber,pageSize);

            //Map Domain Modal to DTOs
            var walkDTOs = mapper.Map<List<WalkDTOs>>(walksDomainModal);

            return Ok(walkDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModal = await walkRepository.GetByIdAsyc(id);

            if(walkDomainModal == null)
            {
                return NotFound();
            }

            //Map domain modal to dto..
           var walkDto =  mapper.Map<WalkDTOs>(walkDomainModal);

            return Ok(walkDto);
        }


        [HttpPost]
        [ValidateModal]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
                //Map DTOs to Domain Modal, using AutoMapper
                var walkDomainModal = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModal);

                //Map domain modal back to DTO..
                var walkDTOs = mapper.Map<WalkDTOs>(walkDomainModal);

                return Ok(walkDTOs);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModal]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
         
                //Map DTOs to Domain Modal, using AutoMapper
                var walkDomainModal = mapper.Map<Walk>(addWalkRequestDto);

                walkDomainModal = await walkRepository.UpdateAsync(id, walkDomainModal);

                if (walkDomainModal == null)
                {
                    return NotFound();
                }

                //Map domain modal back to DTO..
                var walkDTOs = mapper.Map<WalkDTOs>(walkDomainModal);

                return Ok(walkDTOs);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModal = await walkRepository.DeleteAsync(id);

            if(walkDomainModal == null)
            {
                return NotFound();
            }

            //Map domain modal to dto..
            var walkDto = mapper.Map<WalkDTOs>(walkDomainModal);

            return Ok(walkDto);
        }
    }
}
