using Shared.Results;
using Shared;

namespace UI.Interfaces.MessageRequests
{
    /// <summary>
    /// Методы для отправки сообщений
    /// </summary>
    public interface IMessageSendRequests
    {
        /// <summary>
        /// Отправить сообщение пользователю
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SendMessageResult?> SendPush(NewMessageDTO message);
    }
}