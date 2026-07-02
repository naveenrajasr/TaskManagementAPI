using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.BackgroundJobs
{
    public class TaskExpiryBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TaskExpiryBackgroundService> _logger;

        public TaskExpiryBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<TaskExpiryBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var expiredTasks = await context.Tasks
                    .Where(t => t.Status == "Pending" &&
                                t.DueDate < DateTime.Now)
                    .ToListAsync(stoppingToken);

                if (expiredTasks.Any())
                {
                    foreach (var task in expiredTasks)
                    {
                        task.Status = "Expired";
                        task.UpdatedAt = DateTime.Now;
                    }

                    await context.SaveChangesAsync(stoppingToken);

                    _logger.LogInformation(
                        "{Count} task(s) marked as Expired.",
                        expiredTasks.Count);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}