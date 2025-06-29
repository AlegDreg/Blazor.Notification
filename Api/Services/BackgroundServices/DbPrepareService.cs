using Api.Interfaces;

namespace Api.Services.BackgroundServices
{
    /// <summary>
    /// Подготовить БД перед запуском приложения - отчистить все подключения в таблице
    /// </summary>
    public class DbPrepareService(IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            await repo.ClearAllConnections();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}