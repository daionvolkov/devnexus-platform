namespace DevNexus.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Language { get; set; }  
        public int Level { get; set; }     
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; } = null!;
    }
}
