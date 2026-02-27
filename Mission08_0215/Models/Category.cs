namespace Mission08_0215.Models
{
    // Separate Category table to populate the dropdown (as required by assignment)
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property - one category can belong to many tasks
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
