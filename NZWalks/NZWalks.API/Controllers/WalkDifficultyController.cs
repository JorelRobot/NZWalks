using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null) return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty([FromBody] Models.DTO.AddWalkdifficultyRequest addWalkdifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty() {
                Code = addWalkdifficultyRequest.Code,
            };

            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultiyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultiyDTO.Id }, walkDifficultiyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficulty)
        {
            var walkDificultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficulty.Code,
            };

            await walkDifficultyRepository.UpdateAsycn(id, walkDificultyDomain);

            if (walkDificultyDomain == null) return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDificultyDomain);

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null) return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }
    }
}
