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

    }
}

