using System.ComponentModel.DataAnnotations;
using TaskManagement.Application.Validation;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        [EnumDataType(typeof(TaskItemStatus), ErrorMessage = "Invalid task status")]
        public TaskItemStatus Status { get; set; }
        [EnumDataType(typeof(TaskItemPriority), ErrorMessage = "Invalid task priority")]
        public TaskItemPriority Priority { get; set; }
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime DueDate { get; set; }
    }
}
