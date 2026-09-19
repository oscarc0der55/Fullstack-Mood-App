using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoodAppBE.Models;

namespace MoodAppBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await userManager.GetUserAsync(User);
            var roles = await userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Email,
                Role = roles.FirstOrDefault()
            });
        }
        //[Authorize(Roles = "Admin")]
        //[HttpGet("all")]
        //public IActionResult GetAllUsers()
        //{

        //}
    }
}
