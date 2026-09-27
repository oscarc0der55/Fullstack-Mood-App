using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Service.IService;
using Microsoft.AspNetCore.Http;
using MoodAppBE.DTO.Mood;

namespace MoodAppBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase
    {
        private readonly IMoodService moodService;

        public MoodController(IMoodService _moodService)
        {
            moodService = _moodService;
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyMoods()
        {
            var userId = GetCurrentUserId();
            var moods = await moodService.GetMoodsByUserIdAsync(userId);
            return Ok(moods);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<List<MoodDTO>>> GetAll()
        {
            var moods = await moodService.GetMoodsAsync();
            return Ok(moods);
        }

        [Authorize]
        [HttpGet("{moodId:int}")]
        public async Task<ActionResult<MoodDTO>> GetById(int moodId)
        {
            var mood = await moodService.GetMoodByIdAsync(moodId);
            if (mood == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && mood.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(mood);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MoodDTO>> Create(CreateMoodDTO newMood)
        {
            var userId = GetCurrentUserId();
            var createdMood = await moodService.CreateMoodAsync(userId, newMood);
            return CreatedAtAction(nameof(GetById), new { moodId = createdMood.MoodId }, createdMood);
        }

        [Authorize]
        [HttpPut("{moodId:int}")]
        public async Task<IActionResult> Update(int moodId, UpdateMoodDTO mood)
        {
            var existingMood = await moodService.GetMoodByIdAsync(moodId);
            if (existingMood == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && existingMood.UserId != userId)
            {
                return Forbid();
            }

            var update = await moodService.UpdateMoodAsync(moodId, mood);
            if (!update)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{moodId:int}")]
        public async Task<IActionResult> Delete(int moodId)
        {
            var existingMood = await moodService.GetMoodByIdAsync(moodId);
            if (existingMood == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && existingMood.UserId != userId)
            {
                return Forbid();
            }

            var delete = await moodService.DeleteMoodAsync(moodId);
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
