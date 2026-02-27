using Microsoft.EntityFrameworkCore;

namespace Mission08_0215.Models
{
    public class TaskContext : DbContext
    {
        // Constructor passes options to the base DbContext class
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        // These DbSets map to the tables in the SQLite database
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
