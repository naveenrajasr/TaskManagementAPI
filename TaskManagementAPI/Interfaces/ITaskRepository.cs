using TaskManagementAPI.Models;

namespace TaskManagementAPI.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync(string? status);

        Task<TaskItem?> GetByIdAsync(int id);

        Task AddAsync(TaskItem task);

        Task UpdateAsync(TaskItem task);

        Task DeleteAsync(TaskItem task);

        Task SaveChangesAsync();
    }
}