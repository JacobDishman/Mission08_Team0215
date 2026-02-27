using Microsoft.EntityFrameworkCore;

namespace Mission08_0215.Models
{
    // Concrete implementation of ITaskRepository using EF Core + SQLite
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            // Include Category so we can display the category name, not just the ID
            return _context.Tasks.Include(t => t.Category).ToList();
        }

        public TaskItem? GetTaskById(int id)
        {
            return _context.Tasks.Include(t => t.Category).FirstOrDefault(t => t.TaskItemId == id);
        }

        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
