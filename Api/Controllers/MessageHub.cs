using Api.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Shared;
using Shared.Results;

namespace Api.Controllers
{
    public class MessageHub(IUserConnection userConnection, IMessage messageService, IMessageRepository messageRepository) : Hub
    {
        /// <summary>
        /// Запрос с попыткой авторизоваться
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<AuthResult> TryAuthorize(string login)
        {
            string connectionId = Context.ConnectionId;

            var authResult = await userConnection.ConnectUser(login, connectionId);

            return authResult;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Пользователь отправил сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<SendMessageResult?> SendMessage(NewMessageDTO message)
        {
            var pushResult = await messageService.NewMessage(message);

            return pushResult;
        }
        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <param name="messageRequest"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<MessageDTO>> GetMessages(MessageRequestDTO messageRequest)
        {
            return await messageRepository.GetMessages(messageRequest.Skip, messageRequest.Task);
        }
        /// <summary>
        /// Получатель проучитал сообщение и сообщает об этом
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        public async Task MessageReaded(MessageDTO messageDTO)
        {
            await messageService.MessageReaded(messageDTO);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await userConnection.DisconnectUser(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}