using DevNexus.Core.Entities;
using DevNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevNexus.Application.Services
{
    public class LessonService
    {
        private readonly DevNexusDbContext _context;

        public LessonService(DevNexusDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync() => await _context.Lessons.ToListAsync();

        public async Task<Lesson?> GetByIdAsync(int id) => await _context.Lessons.FindAsync(id);


        public async Task<Lesson> CreateAsync(Lesson lesson)
        {
            lesson.CreatedAt = DateTime.UtcNow;
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }


        public async Task<bool> UpdateAsync(Lesson lesson)
        {
            var existing = await _context.Lessons.FindAsync(lesson.Id);
            if (existing == null) return false;
            existing.Title = lesson.Title;
           
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return false;
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
