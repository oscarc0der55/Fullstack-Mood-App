using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Service.IService;
using Microsoft.AspNetCore.Http;
using MoodAppBE.DTO.Wellness;

namespace MoodAppBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellnessController : ControllerBase
    {
        private readonly IWellnessService wellnessService;

        public WellnessController(IWellnessService _wellnessService)
        {
            wellnessService = _wellnessService;
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyWellness()
        {
            var userId = GetCurrentUserId();
            var wellness = await wellnessService.GetWellnessByUserIdAsync(userId);
            return Ok(wellness);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<List<WellnessDTO>>> GetAll()
        {
            var wellness = await wellnessService.GetWellnessAsync();
            return Ok(wellness);
        }

        [Authorize]
        [HttpGet("{wellnessId:int}")]
        public async Task<ActionResult<WellnessDTO>> GetById(int wellnessId)
        {
            var wellness = await wellnessService.GetWellnessByIdAsync(wellnessId);
            if (wellness == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && wellness.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(wellness);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WellnessDTO>> Create(CreateWellnessDTO newWellness)
        {
            var userId = GetCurrentUserId();
            var createdWellness = await wellnessService.CreateWellnessAsync(userId, newWellness);
            return CreatedAtAction(nameof(GetById), new { wellnessId = createdWellness.WellnessId }, createdWellness);
        }

        [Authorize]
        [HttpPut("{wellnessId:int}")]
        public async Task<IActionResult> Update(int wellnessId, UpdateWellnessDTO wellness)
        {
            var existingWellness = await wellnessService.GetWellnessByIdAsync(wellnessId);
            if (existingWellness == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && existingWellness.UserId != userId)
            {
                return Forbid();
            }

            var update = await wellnessService.UpdateWellnessAsync(wellnessId, wellness);

            if (!update)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{wellnessId:int}")]
        public async Task<IActionResult> Delete(int wellnessId)
        {
            var existingWellness = await wellnessService.GetWellnessByIdAsync(wellnessId);
            if (existingWellness == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && existingWellness.UserId != userId)
            {
                return Forbid();
            }

            var delete = await wellnessService.DeleteWellnessAsync(wellnessId);
            if (!delete)
            {
                return NotFound();
            }
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }
    }
}
