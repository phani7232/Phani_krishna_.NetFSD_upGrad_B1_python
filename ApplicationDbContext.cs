using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Fulfills the "Configure DbContext with all DbSets" requirement
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Enforce UNIQUE Email requirement using Fluent API
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 2. Prevent SQL Server "Multiple Cascade Paths" error
            // Because deleting a User deletes a Result, AND deleting a Course deletes a Quiz which deletes a Result,
            // SQL Server will block the migration unless we restrict one of those paths.
            modelBuilder.Entity<Result>()
                .HasOne(r => r.User)
                .WithMany(u => u.Results)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}