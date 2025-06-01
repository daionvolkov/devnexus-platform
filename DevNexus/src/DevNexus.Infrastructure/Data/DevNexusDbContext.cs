using DevNexus.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevNexus.Infrastructure.Data
{
    public class DevNexusDbContext : DbContext
    {
        public DevNexusDbContext(DbContextOptions<DevNexusDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Email).IsRequired().HasMaxLength(128).HasColumnName("email");
                entity.Property(u => u.PasswordHash).IsRequired().HasColumnName("password_hash");
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100).HasColumnName("full_name");
                entity.Property(u => u.Role).HasConversion<int>().IsRequired().HasColumnName("role");
                entity.Property(u => u.RegisteredAt).HasDefaultValueSql("now()").HasColumnName("registered_at");


            });

            // Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Description).HasMaxLength(2000);
                entity.Property(c => c.Status).HasConversion<int>().IsRequired();
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("now()");

                entity.HasOne(c => c.Author)
                      .WithMany()
                      .HasForeignKey(c => c.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("enrollments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasConversion<int>().IsRequired();
                entity.Property(e => e.EnrolledAt).HasDefaultValueSql("now()");

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("lessons");

                entity.HasKey(l => l.Id);

                entity.Property(l => l.Id)
                    .HasColumnName("id");

                entity.Property(l => l.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(l => l.Language)
                    .HasColumnName("language")
                    .IsRequired();

                entity.Property(l => l.Level)
                    .HasColumnName("level")
                    .IsRequired();

                entity.Property(l => l.Content)
                    .HasColumnName("content");

                entity.Property(l => l.AuthorId)
                    .HasColumnName("author_id")
                    .IsRequired();

                entity.Property(l => l.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRequired();

                entity.HasOne(l => l.Author)
                    .WithMany(u => u.Lessons)
                    .HasForeignKey(l => l.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

        }
    }
}
