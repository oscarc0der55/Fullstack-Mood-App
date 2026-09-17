using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Service.IService;
using Microsoft.AspNetCore.Http;
using MoodAppBE.DTO.Wellness;

namespace MoodAppBE.Controllers
{
    [Route("api/[controllers]")]
    [ApiController]
    public class WellnessController : ControllerBase
    {
        private readonly IWellnessService wellnessService;

        public WellnessController(IWellnessService _wellnessService)
        {
            wellnessService = _wellnessService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<WellnessDTO>>> GetAll()
        {
            var wellness = await wellnessService.GetWellnessAsync();
            return Ok(wellness);
        }

        [HttpGet]
        [Route("{wellnessId:int}")]
        public async Task<ActionResult<WellnessDTO>> GetById(int wellnessId)
        {
            var wellness = await wellnessService.GetWellnessByIdAsync(wellnessId);
            if (wellness == null)
            {
                return NotFound();
            }

            return Ok(wellness);
        }

        [HttpPost]
        public async Task<ActionResult<WellnessDTO>> Create(CreateWellnessDTO newWellness)
        {
            var createdWellness = await wellnessService.CreateWellnessAsync(newWellness);
            return CreatedAtAction(nameof(GetById), new { wellnessId = createdWellness.WellnessId }, createdWellness);
        }

        [HttpPut]
        [Route("{wellnessId:int}")]
        public async Task<IActionResult> Update(int wellnessId, UpdateWellnessDTO wellness)
        {
            var update = await wellnessService.UpdateWellnessAsync(wellnessId, wellness);

            if (!update)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{wellnessId:int}")]
        public async Task<IActionResult> Delete(int wellnessId)
        {
            var delete = await wellnessService.DeleteWellnessAsync(wellnessId);
            if (!delete)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}