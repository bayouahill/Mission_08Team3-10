using System.ComponentModel.DataAnnotations;

namespace Mission_08Team3_10.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
