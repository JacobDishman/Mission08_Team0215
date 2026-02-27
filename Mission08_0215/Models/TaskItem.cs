using System.ComponentModel.DataAnnotations;

namespace Mission08_0215.Models
{
    // Named TaskItem to avoid conflict with System.Threading.Tasks.Task
    public class TaskItem
    {
        public int TaskItemId { get; set; }

        // Foreign key to Categories table
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        public string TaskName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        public bool Completed { get; set; } = false;

        // Navigation property for the related Category
        public Category? Category { get; set; }
    }
}
