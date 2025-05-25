using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return _mapper.Map<List<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemDto?> GetByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? null : _mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskDto input)
        {
            var task = _mapper.Map<TaskItem>(input);
            task.CreatedAt = DateTime.UtcNow;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDto input)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _mapper.Map(input, task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
