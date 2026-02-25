using System.ComponentModel.DataAnnotations;

namespace Mission_08Team3_10.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Task")]
        public string TaskName { get; set; } = string.Empty;

        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [Required]
        [Range(1, 4)]
        public int Quadrant { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool Completed { get; set; }
    }
}
