using Api.Data.Entity;
using Shared;
using Shared.Results;

namespace Api.Interfaces
{
    /// <summary>
    /// Описание методов для взаимодействия с SignalR клиента
    /// </summary>
    public interface ISignalrRequest
    {
        /// <summary>
        /// Отправить пользователю результат авторизации
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="authResult"></param>
        /// <returns></returns>
        Task SendAuthResult(string connectionId, AuthResult authResult);
        /// <summary>
        /// Отправить пуш сообщение пользователю
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<Ping> SendPush(string connectionId, MessageDTO message);
        /// <summary>
        /// Отправить сообщение с результатом доставки пуша
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="pushResult"></param>
        /// <returns></returns>
        Task SendPushResult(string connectionId, SendMessageResult pushResult);
    }
}
