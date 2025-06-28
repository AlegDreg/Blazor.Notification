using UI.Interfaces.MessageActions;

namespace UI.Services
{
    /// <summary>
    /// Обработка фонового сообщения из бэка
    /// </summary>
    public class BackgroundMessageRouter : IDisposable
    {
        private readonly IMessageTracking messageTracking;
        private readonly IEnumerable<IBackgroundMessageHandle> messageHandle;

        public BackgroundMessageRouter(IMessageTracking messageTracking, IEnumerable<IBackgroundMessageHandle> messageHandle)
        {
            this.messageTracking = messageTracking;
            this.messageHandle = messageHandle;
            this.messageTracking.NewBackgroundMessage += MessageTracking_NewBackgroundMessage;
        }

        private async void MessageTracking_NewBackgroundMessage(object? sender, string message)
        {
            await Handle(message);
        }
        /// <summary>
        /// Обработать сообщение через все реализации <see cref="IBackgroundMessageHandle"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task Handle(string message)
        {
            foreach (var h in messageHandle)
                await h.Handle(message);
        }

        ~BackgroundMessageRouter()
        {
            Dispose();
        }

        public void Dispose()
        {
            messageTracking.NewBackgroundMessage -= MessageTracking_NewBackgroundMessage;
            GC.SuppressFinalize(this);
        }
    }
}