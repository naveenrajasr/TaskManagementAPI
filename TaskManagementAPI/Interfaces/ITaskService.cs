using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(string? status);

        Task<TaskResponseDto?> GetTaskByIdAsync(int id);

        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);

        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);

        Task<bool> DeleteTaskAsync(int id);
    }
}