using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Mappings;
using TaskManagement.Infrastructure.Services;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly IMapper _mapper;
        
        public TaskServiceTests()
        {
            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            var context = new ApplicationDbContext(options);
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var context = GetDbContext();
            var taskService = new TaskService(context, _mapper);
            
            var tasks = new List<TaskItem>
            {
                new TaskItem 
                { 
                    Id = 1, 
                    Title = "Task 1", 
                    Description = "Description 1",
                    Status = TaskItemStatus.NotStarted,
                    Priority = TaskItemPriority.Medium,
                    DueDate = DateTime.UtcNow.AddDays(1)
                },
                new TaskItem 
                { 
                    Id = 2, 
                    Title = "Task 2", 
                    Description = "Description 2",
                    Status = TaskItemStatus.InProgress,
                    Priority = TaskItemPriority.High,
                    DueDate = DateTime.UtcNow.AddDays(2)
                }
            };
            
            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
            
            // Act
            var result = await taskService.GetAllAsync();
            
            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            var context = GetDbContext();
            var taskService = new TaskService(context, _mapper);
            
            var taskItem = new TaskItem 
            { 
                Id = 1, 
                Title = "Test Task", 
                Description = "Test Description",
                Status = TaskItemStatus.NotStarted,
                Priority = TaskItemPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(1)
            };
            
            await context.Tasks.AddAsync(taskItem);
            await context.SaveChangesAsync();
            
            // Act
            var result = await taskService.GetByIdAsync(1);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAndReturnTask()
        {
            // Arrange
            var context = GetDbContext();
            var taskService = new TaskService(context, _mapper);
            
            var createTaskDto = new CreateTaskDto 
            { 
                Title = "New Task",
                Description = "New Description",
                Status = TaskItemStatus.NotStarted,
                Priority = TaskItemPriority.Low,
                DueDate = DateTime.UtcNow.AddDays(3)
            };
            
            // Act
            var result = await taskService.CreateAsync(createTaskDto);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Task", result.Title);
            Assert.Equal(1, await context.Tasks.CountAsync());
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenTaskUpdated()
        {
            // Arrange
            var context = GetDbContext();
            var taskService = new TaskService(context, _mapper);
            
            var taskItem = new TaskItem 
            { 
                Id = 1, 
                Title = "Original Task", 
                Description = "Original Description",
                Status = TaskItemStatus.NotStarted,
                Priority = TaskItemPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(1)
            };
            
            await context.Tasks.AddAsync(taskItem);
            await context.SaveChangesAsync();
            
            var updateTaskDto = new UpdateTaskDto
            {
                Title = "Updated Task",
                Description = "Updated Description",
                Status = TaskItemStatus.InProgress,
                Priority = TaskItemPriority.High
            };
            
            // Act
            var result = await taskService.UpdateAsync(1, updateTaskDto);
            var updatedTask = await context.Tasks.FindAsync(1);
            
            // Assert
            Assert.True(result);
            Assert.Equal("Updated Task", updatedTask.Title);
            Assert.Equal("Updated Description", updatedTask.Description);
            Assert.Equal(TaskItemStatus.InProgress, updatedTask.Status);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenTaskDeleted()
        {
            // Arrange
            var context = GetDbContext();
            var taskService = new TaskService(context, _mapper);
            
            var taskItem = new TaskItem { Id = 1, Title = "Test Task" };
            await context.Tasks.AddAsync(taskItem);
            await context.SaveChangesAsync();
            
            // Act
            var result = await taskService.DeleteAsync(1);
            
            // Assert
            Assert.True(result);
            Assert.Equal(0, await context.Tasks.CountAsync());
        }
    }
}
