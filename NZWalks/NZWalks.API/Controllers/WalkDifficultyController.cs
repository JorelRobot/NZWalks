using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null) return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficulty([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkdifficultyRequest)
        {
            // Validate incoming data
            // if (!ValidateAddWalkDifficulty(addWalkdifficultyRequest)) return BadRequest(ModelState);

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty() {
                Code = addWalkdifficultyRequest.Code,
            };

            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultiyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultiyDTO.Id }, walkDifficultiyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficulty)
        {

            // Validate incoming data
            // if (!ValidateUpdateWalkDifficulty(updateWalkDifficulty)) return BadRequest(ModelState);

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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null) return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficulty(Models.DTO.AddWalkDifficultyRequest addWalkdifficultyRequest)
        {
            if (addWalkdifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkdifficultyRequest),
                    $"{nameof(addWalkdifficultyRequest)} is required");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkdifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkdifficultyRequest.Code), 
                    $"{nameof(addWalkdifficultyRequest.Code)} cannot be Null or Empty Spaces");
            }

            if (ModelState.ErrorCount > 0) return false;

            return true;
        }

        private bool ValidateUpdateWalkDifficulty(Models.DTO.UpdateWalkDifficultyRequest updateWalkdifficultyRequest)
        {
            if (updateWalkdifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkdifficultyRequest),
                    $"{nameof(updateWalkdifficultyRequest)} is required");

                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkdifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkdifficultyRequest.Code),
                    $"{nameof(updateWalkdifficultyRequest.Code)} cannot be Null or Empty Spaces");
            }

            if (ModelState.ErrorCount > 0) return false;

            return true;
        }
        #endregion
    }
}
