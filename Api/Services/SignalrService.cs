using Api.Controllers;
using Api.Data.Entity;
using Api.Interfaces.MessageRequests;
using Microsoft.AspNetCore.SignalR;
using Shared;

namespace Api.Services
{
    /// <summary>
    /// Сервис для отправки сообщений по SignalR
    /// </summary>
    /// <param name="hub"></param>
    public class SignalrService(IHubContext<MessageHub> hub) : IMessageNotifyRequests, IMessageDeliveryRequests
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="exceptConnectionId">Исключаемый айди подключения</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendNotifyPush(string exceptConnectionId, MessageDTO message)
        {
            return hub.Clients.AllExcept(exceptConnectionId).SendAsync("NotifyPush", message);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId">Айди подключения</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public Task SendPush(string connectionId, MessageDTO message)
        {
            return hub.Clients.Client(connectionId).SendAsync("NewUserMessage", message, CancellationToken.None);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendReadedNotify(MessageDTO message, User user)
        {
            return hub.Clients.Client(user.ConnectionId!).SendAsync("NotifyRead", message);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendReadedNotify(MessageDTO message)
        {
            return hub.Clients.All.SendAsync("NotifyRead", message);
        }
    }
}