using Microsoft.AspNetCore.SignalR.Client;

namespace UI.Interfaces
{
    /// <summary>
    /// Общие методы для веб запросов
    /// </summary>
    public interface IWebRequest
    {
        /// <summary>
        /// Подключиться
        /// </summary>
        /// <returns></returns>
        Task Connect();
        HubConnection HubConnection { get; }
    }
}