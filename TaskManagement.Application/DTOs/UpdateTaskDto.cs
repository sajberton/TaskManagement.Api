using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskItemStatus? Status { get; set; }
        public TaskItemPriority? Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
