using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync();
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<TaskItemDto> CreateAsync(CreateTaskDto input);
        Task<bool> UpdateAsync(int id, UpdateTaskDto input);
        Task<bool> DeleteAsync(int id);
    }
}
