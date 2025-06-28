using UI.Interfaces;

namespace UI.Services
{
    public class AppInitializer : IAppInitializer
    {
        private readonly BackgroundMessageRouter backgroundMessage;
        private readonly IWebRequest web;

        /*
         
        Создаёnтся экземпляр BackgroundMessageRouter
        Выполняется подключение SignalR

         */

        public AppInitializer(BackgroundMessageRouter backgroundMessage, IWebRequest web)
        {
            this.backgroundMessage = backgroundMessage;
            this.web = web;
        }

        public async Task InitAsync()
        {
            await web.Connect();
        }
    }
}