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
        public MoodController (IMoodService _moodService)
        {
            moodService = _moodService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MoodDTO>>> GetAll()
        {
            var moods = await moodService.GetMoodsAsync();

            return Ok(moods);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{moodId:int}")]
        public async Task<ActionResult<MoodDTO>> GetById(int moodId)
        {
            var mood = await moodService.GetMoodByIdAsync(moodId);
            if(mood == null)
            {
                return NotFound();
            }

            return Ok(mood);
        }

        [HttpPost]
        public async Task<ActionResult<MoodDTO>> Create(CreateMoodDTO newMood)
        {
            var createdMood = await moodService.CreateMoodAsync(newMood);
            return CreatedAtAction(nameof(GetById), new { moodId = createdMood.MoodId }, createdMood);
        }
        [HttpPut]
        [Route("{moodId:int}")]
        public async Task<IActionResult> Update(int moodId, UpdateMoodDTO mood)
        {
            var update = await moodService.UpdateMoodAsync(moodId, mood);

            if (!update)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{moodId:int}")]
        public async Task<IActionResult> Delete(int moodId)
        {
            var delete = await moodService.DeleteMovieAsync(moodId);
            if (!delete)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
