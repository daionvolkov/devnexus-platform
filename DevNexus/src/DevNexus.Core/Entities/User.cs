using DevNexus.Core.Enums;

namespace DevNexus.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public UserRoleEnum Role { get; set; }
        public DateTime RegisteredAt { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
