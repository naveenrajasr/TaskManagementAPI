using TaskManagementAPI.DTOs;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using TaskManagementAPI.Exceptions;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TaskService> _logger;
        public TaskService(
     ITaskRepository repository,
     ILogger<TaskService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(string? status)
        {
            var tasks = await _repository.GetAllAsync(status);

            return tasks.Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            });
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                return null;

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new BadRequestException("Title is required.");
            if (dto.DueDate < DateTime.Now)
                throw new BadRequestException("Due date cannot be in the past.");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _repository.AddAsync(task);
            await _repository.SaveChangesAsync();
            _logger.LogInformation(
    "Task created. Id: {TaskId}, Title: {Title}",
    task.Id,
    task.Title);
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
            {
                _logger.LogWarning("Update failed. Task with Id {TaskId} not found.", id);
                return false;
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Status = dto.Status;
            task.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(task);
            await _repository.SaveChangesAsync();
            _logger.LogInformation(
    "Task updated successfully. Id: {TaskId}, Title: {Title}",
    task.Id,
    task.Title);

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
            {
                _logger.LogWarning("Update failed. Task with Id {TaskId} not found.", id);
                return false;
            }

            await _repository.DeleteAsync(task);
            await _repository.SaveChangesAsync();
            _logger.LogInformation(
    "Task deleted successfully. Id: {TaskId}, Title: {Title}",
    task.Id,
    task.Title);
            return true;
        }
    }
}