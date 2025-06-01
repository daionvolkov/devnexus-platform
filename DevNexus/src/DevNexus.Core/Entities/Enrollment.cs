using DevNexus.Core.Enums;

namespace DevNexus.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public EnrollmentStatusEnum Status { get; set; }
        public DateTime EnrolledAt { get; set; }
    }
}
