using Microsoft.EntityFrameworkCore;

namespace Mission_08Team3_10.Models
{
    public class Mission08Context : DbContext
    {

        public Mission08Context(DbContextOptions<Mission08Context> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );

            modelBuilder.Entity<Task>().HasData(
                new Task { TaskId = 1, TaskName = "Pay Bills", DueDate = new DateOnly(2026, 3, 1), Quadrant = 1, CategoryId = 1, Completed = false },
                new Task { TaskId = 2, TaskName = "Study for Exam", DueDate = new DateOnly(2026, 3, 5), Quadrant = 1, CategoryId = 2, Completed = false },
                new Task { TaskId = 3, TaskName = "Exercise", DueDate = null, Quadrant = 2, CategoryId = 1, Completed = false },
                new Task { TaskId = 4, TaskName = "Read Scriptures", DueDate = null, Quadrant = 2, CategoryId = 4, Completed = false },
                new Task { TaskId = 5, TaskName = "Answer Emails", DueDate = new DateOnly(2026, 3, 1), Quadrant = 3, CategoryId = 3, Completed = false },
                new Task { TaskId = 6, TaskName = "Browse Social Media", DueDate = null, Quadrant = 4, CategoryId = null, Completed = false }
            );
        }

    }
}

