namespace Mission08_0215.Models
{
    // Repository Pattern interface - defines the contract for data access
    public interface ITaskRepository
    {
        // Task operations
        IEnumerable<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(int id);

        // Category operations
        IEnumerable<Category> GetAllCategories();
    }
}
