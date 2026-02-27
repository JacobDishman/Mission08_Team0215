using Microsoft.EntityFrameworkCore;

namespace Mission08_0215.Models
{
    // Populates the database with initial categories and sample tasks
    public static class SeedData
    {
        public static void Initialize(TaskContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Only seed if there are no categories already
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = "Home" },
                    new Category { CategoryName = "School" },
                    new Category { CategoryName = "Work" },
                    new Category { CategoryName = "Church" }
                );
                context.SaveChanges();
            }

            // Only seed if there are no tasks already
            if (!context.Tasks.Any())
            {
                // Get category IDs after seeding
                var home = context.Categories.First(c => c.CategoryName == "Home").CategoryId;
                var school = context.Categories.First(c => c.CategoryName == "School").CategoryId;
                var work = context.Categories.First(c => c.CategoryName == "Work").CategoryId;
                var church = context.Categories.First(c => c.CategoryName == "Church").CategoryId;

                context.Tasks.AddRange(
                    // Quadrant I: Important / Urgent
                    new TaskItem
                    {
                        TaskName = "Submit IS413 project",
                        DueDate = DateTime.Today.AddDays(1),
                        Quadrant = 1,
                        CategoryId = school,
                        Completed = false
                    },
                    new TaskItem
                    {
                        TaskName = "Pay rent",
                        DueDate = DateTime.Today,
                        Quadrant = 1,
                        CategoryId = home,
                        Completed = false
                    },

                    // Quadrant II: Important / Not Urgent
                    new TaskItem
                    {
                        TaskName = "Study for final exams",
                        DueDate = DateTime.Today.AddDays(14),
                        Quadrant = 2,
                        CategoryId = school,
                        Completed = false
                    },
                    new TaskItem
                    {
                        TaskName = "Prepare quarterly report",
                        DueDate = DateTime.Today.AddDays(10),
                        Quadrant = 2,
                        CategoryId = work,
                        Completed = false
                    },

                    // Quadrant III: Not Important / Urgent
                    new TaskItem
                    {
                        TaskName = "Respond to non-critical emails",
                        DueDate = DateTime.Today,
                        Quadrant = 3,
                        CategoryId = work,
                        Completed = false
                    },
                    new TaskItem
                    {
                        TaskName = "Attend optional team meeting",
                        DueDate = DateTime.Today.AddDays(2),
                        Quadrant = 3,
                        CategoryId = work,
                        Completed = false
                    },

                    // Quadrant IV: Not Important / Not Urgent
                    new TaskItem
                    {
                        TaskName = "Reorganize bookshelf",
                        DueDate = null,
                        Quadrant = 4,
                        CategoryId = home,
                        Completed = false
                    },
                    new TaskItem
                    {
                        TaskName = "Browse social media",
                        DueDate = null,
                        Quadrant = 4,
                        CategoryId = null,
                        Completed = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
