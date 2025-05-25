using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Api.Controllers;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Enums;
using Xunit;

namespace TaskManagement.Tests.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _mockTaskService;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockTaskService = new Mock<ITaskService>();
            _controller = new TasksController(_mockTaskService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithTasks()
        {
            // Arrange
            var tasks = new List<TaskItemDto>
            {
                new TaskItemDto { Id = 1, Title = "Task 1" },
                new TaskItemDto { Id = 2, Title = "Task 2" }
            };
            
            _mockTaskService.Setup(svc => svc.GetAllAsync())
                .ReturnsAsync(tasks);
            
            // Act
            var result = await _controller.GetAll();
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TaskItemDto>>(okResult.Value);
            Assert.Equal(2, ((List<TaskItemDto>)returnValue).Count);
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenTaskExists()
        {
            // Arrange
            var task = new TaskItemDto { Id = 1, Title = "Task 1" };
            _mockTaskService.Setup(svc => svc.GetByIdAsync(1))
                .ReturnsAsync(task);
            
            // Act
            var result = await _controller.Get(1);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TaskItemDto>(okResult.Value);
            Assert.Equal("Task 1", returnValue.Title);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            // Arrange
            _mockTaskService.Setup(svc => svc.GetByIdAsync(1))
                .ReturnsAsync((TaskItemDto)null);
            
            // Act
            var result = await _controller.Get(1);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateTaskDto 
            { 
                Title = "New Task",
                Description = "Description",
                Status = TaskItemStatus.NotStarted,
                Priority = TaskItemPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(1)
            };
            
            var createdTask = new TaskItemDto
            {
                Id = 1,
                Title = "New Task",
                Description = "Description"
            };
            
            _mockTaskService.Setup(svc => svc.CreateAsync(It.IsAny<CreateTaskDto>()))
                .ReturnsAsync(createdTask);
            
            // Act
            var result = await _controller.Create(createDto);
            
            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenTaskUpdated()
        {
            // Arrange
            var updateDto = new UpdateTaskDto { Title = "Updated Task" };
            
            _mockTaskService.Setup(svc => svc.UpdateAsync(1, It.IsAny<UpdateTaskDto>()))
                .ReturnsAsync(true);
            
            // Act
            var result = await _controller.Update(1, updateDto);
            
            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenTaskDeleted()
        {
            // Arrange
            _mockTaskService.Setup(svc => svc.DeleteAsync(1))
                .ReturnsAsync(true);
            
            // Act
            var result = await _controller.Delete(1);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
