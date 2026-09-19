using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Service.IService;

namespace MoodAppBE.Controllers
{
    [ApiController]
    [Route("api/user-wellness")]
    public class UserWellnessController : ControllerBase
    {
        private readonly IUserWellnessService
            userWellnessService;

        public UserWellnessController(
            IUserWellnessService userWellnessService)
        {
            this.userWellnessService =
                userWellnessService;
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyWellness()
        {
            var userId = GetCurrentUserId();

            var wellness =
                await userWellnessService
                    .GetByUserIdAsync(userId);

            return Ok(wellness);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWellness()
        {
            var wellness =
                await userWellnessService.GetAllAsync();

            return Ok(wellness);
        }

        [Authorize]
        [HttpGet("{usersWellnessId:int}")]
        public async Task<IActionResult> GetById(
            int usersWellnessId)
        {
            var wellness =
                await userWellnessService
                    .GetByIdAsync(usersWellnessId);

            if (wellness == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin &&
                wellness.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(wellness);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int wellnessId)
        {
            var userId = GetCurrentUserId();

            var wellness =
                await userWellnessService.CreateAsync(
                    userId,
                    wellnessId);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    usersWellnessId =
                        wellness.UsersWellnessId
                },
                wellness);
        }

        [Authorize]
        [HttpDelete("{usersWellnessId:int}")]
        public async Task<IActionResult> Delete(int usersWellnessId)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            var deleted =
                await userWellnessService.DeleteAsync(
                    usersWellnessId,
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