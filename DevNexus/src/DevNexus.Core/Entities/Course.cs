using DevNexus.Core.Enums;

namespace DevNexus.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public CourseStatusEnum Status { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
