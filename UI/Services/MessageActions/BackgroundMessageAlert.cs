using UI.Interfaces;
using UI.Interfaces.MessageActions;

namespace UI.Services.MessageActions
{
    /// <summary>
    /// Сервис для отображения Alert от фоновой службы
    /// </summary>
    public class BackgroundMessageAlert : IBackgroundMessageHandle
    {
        private readonly IJsService js;

        public BackgroundMessageAlert(IJsService js)
        {
            this.js = js;
        }

        public Task Handle(string message)
        {
            return js.SendAlert(message, "IHostedService");
        }
    }
}