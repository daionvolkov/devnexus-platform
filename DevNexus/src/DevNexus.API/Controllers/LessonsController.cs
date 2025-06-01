using DevNexus.Application.Services;
using DevNexus.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevNexus.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {

        private readonly LessonService _lessonService;

        public LessonsController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
          => Ok(await _lessonService.GetAllLessonsAsync());



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            return lesson == null ? NotFound() : Ok(lesson);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Lesson lesson)
        {
            var created = await _lessonService.CreateAsync(lesson);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Lesson lesson)
        {
            if (id != lesson.Id) return BadRequest();
            var updated = await _lessonService.UpdateAsync(lesson);
            return updated ? NoContent() : NotFound();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lessonService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
