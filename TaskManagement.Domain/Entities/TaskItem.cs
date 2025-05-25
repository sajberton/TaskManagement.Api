using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TaskItemStatus Status { get; set; } = TaskItemStatus.NotStarted;
        public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
        public DateTime DueDate { get; set; }
    }
}
