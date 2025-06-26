using Api.Data.Entity;
using Api.Interfaces;
using Shared;
using Shared.Results;

namespace Api.Services
{
    /// <summary>
    /// Сервис для управления сообщениями пользователей
    /// </summary>
    /// <param name="messageHub"></param>
    /// <param name="messageRepository"></param>
    /// <param name="userRepository"></param>
    public class MessageService(ISignalrRequest request, IMessageRepository messageRepository, IUserRepository userRepository) : IMessage
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        public async Task<SendMessageResult> NewMessage(MessageDTO messageDTO)
        {
            User? user = await userRepository.GetUserByLogin(messageDTO.ToLogin);

            if (user == null)
                return new SendMessageResult { ErrorMessage = "Адресат не найден" };

            var message = await messageRepository.SaveMessage(messageDTO, user);
            if (message == null)
                return new SendMessageResult { ErrorMessage = "Неудалось сохранить сообщение" };

            return await SendMessage(message);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<SendMessageResult> SendMessage(Message message)
        {
            ArgumentNullException.ThrowIfNull(message.To);

            if (message.To.ConnectionId == null)
                return new SendMessageResult { ErrorMessage = "Пользователь не подключен" };

            Ping result = await request.SendPush(message.To.ConnectionId, (MessageDTO)message);

            if (result != null && result.Success)
            {
                await messageRepository.MessageDelivered(message);
                return new SendMessageResult();
            }
            else
                return new SendMessageResult { ErrorMessage = "Неудалось отправить сообщение" };
        }
    }
}