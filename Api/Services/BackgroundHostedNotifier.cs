using Api.Interfaces.MessageRequests;
using Timer = System.Timers.Timer;

namespace Api.Services
{
    /// <summary>
    /// Сервис для отправки всем клиентам сообщения раз в заданный интервал
    /// </summary>
    /// <param name="provider"></param>
    public class BackgroundHostedNotifier(IServiceProvider provider) : IHostedService, IDisposable
    {
        /// <summary>
        /// Интервал в мс между отправками сообщений
        /// </summary>
        private const int TimerDelay = 30_000; // раз в 30 секунд
        /// <summary>
        /// Сообщение для всех пользователей
        /// </summary>
        private const string Message = "Сообщение из IHostedService 🐱‍🏍";

        private readonly Timer timer = new(TimerDelay);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            return Task.CompletedTask;
        }

        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await NotifyUsers();
        }

        /// <summary>
        /// Отправить всем подключенным пользователям сообщение <see cref="Message"/>
        /// </summary>
        private async Task NotifyUsers()
        {
            try
            {
                using var scope = provider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBackgroundMessages>();

                await service.SendMessageToAll(Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer.Elapsed -= Timer_Elapsed;
            timer.Dispose();
        }
    }
}
