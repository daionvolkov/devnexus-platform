using DevNexus.Application.Services;
using DevNexus.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevNexus.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController(LessonService lessonService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
          => Ok(await lessonService.GetAllLessonsAsync());



        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var lesson = await lessonService.GetByIdAsync(id);
            return lesson == null ? NotFound() : Ok(lesson);
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Lesson lesson)
        {
            var created = await lessonService.CreateAsync(lesson);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Lesson lesson)
        {
            if (id != lesson.Id) return BadRequest();
            var updated = await lessonService.UpdateAsync(lesson);
            return updated ? NoContent() : NotFound();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await lessonService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
