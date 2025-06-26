using Api.Controllers;
using Api.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Shared;
using Shared.Results;

namespace Api.Services
{
    /// <summary>
    /// Сервис для отправки сообщений по SignalR
    /// </summary>
    /// <param name="hub"></param>
    public class SignalrService(IHubContext<MessageHub> hub) : ISignalrRequest
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendAuthResult(string connectionId, AuthResult authResult)
        {
            return hub.Clients.Client(connectionId).SendCoreAsync("AuthResult", [authResult]);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId">Айди подключения</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public Task<Ping> SendPush(string connectionId, MessageDTO message)
        {
            return hub.Clients.Client(connectionId).InvokeCoreAsync<Ping>("NewUserMessage", [message], CancellationToken.None);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="pushResult"></param>
        /// <returns></returns>
        public Task SendPushResult(string connectionId, SendMessageResult pushResult)
        {
            return hub.Clients.Client(connectionId).SendCoreAsync("MessageResult", [pushResult]);
        }
    }
}