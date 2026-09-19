using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Service.IService;

namespace MoodAppBE.Controllers
{
    [ApiController]
    [Route("api/user-moods")]
    public class UserMoodController : ControllerBase
    {
        private readonly IUserMoodService userMoodService;

        public UserMoodController(IUserMoodService userMoodService)
        {
            this.userMoodService = userMoodService;
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyMoods()
        {
            var userId = GetCurrentUserId();

            var moods = await userMoodService.GetByUserIdAsync(userId);

            return Ok(moods);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMoods()
        {
            var moods = await userMoodService.GetAllAsync();

            return Ok(moods);
        }

        [Authorize]
        [HttpGet("{usersMoodId:int}")]
        public async Task<IActionResult> GetById(int usersMoodId)
        {
            var mood = await userMoodService.GetByIdAsync(usersMoodId);

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
        public async Task<IActionResult> Create(int moodId)
        {
            var userId = GetCurrentUserId();

            var mood = await userMoodService.CreateAsync(
                userId,
                moodId);

            return CreatedAtAction(
                nameof(GetById),
                new { usersMoodId = mood.UsersMoodId },
                mood);
        }

        [Authorize]
        [HttpDelete("{usersMoodId:int}")]
        public async Task<IActionResult> Delete(int usersMoodId)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            var deleted = await userMoodService.DeleteAsync(
                usersMoodId,
                userId,
                isAdmin);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userIdValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }
    }
}