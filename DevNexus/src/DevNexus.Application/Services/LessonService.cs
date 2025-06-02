using DevNexus.Core.Entities;
using DevNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevNexus.Application.Services
{
    public abstract class LessonService(DevNexusDbContext context)
    {
        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync() => await context.Lessons.ToListAsync();

        public async Task<Lesson?> GetByIdAsync(int id) => await context.Lessons.FindAsync(id);


        public async Task<Lesson> CreateAsync(Lesson lesson)
        {
            lesson.CreatedAt = DateTime.UtcNow;
            context.Lessons.Add(lesson);
            await context.SaveChangesAsync();
            return lesson;
        }


        public async Task<bool> UpdateAsync(Lesson lesson)
        {
            var existing = await context.Lessons.FindAsync(lesson.Id);
            if (existing == null) return false;
            existing.Title = lesson.Title;
           
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var lesson = await context.Lessons.FindAsync(id);
            if (lesson == null) return false;
            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
