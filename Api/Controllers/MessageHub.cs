using Api.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Shared;

namespace Api.Controllers
{
    public class MessageHub(IUserConnection userConnection, IMessage messageService, ISignalrRequest signalrRequest) : Hub
    {
        /// <summary>
        /// Запрос с попыткой авторизоваться
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task TryAuthorize(string login)
        {
            string connectionId = Context.ConnectionId;

            var authResult = await userConnection.ConnectUser(login, connectionId);

            await signalrRequest.SendAuthResult(connectionId, authResult);
        }

        /// <summary>
        /// Пользователь отправил сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(MessageDTO message)
        {
            var pushResult = await messageService.NewMessage(message);

            await signalrRequest.SendPushResult(Context.ConnectionId, pushResult);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await userConnection.DisconnectUser(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}