using Api.Data.Entity;
using Shared;
using Shared.Results;

namespace Api.Interfaces
{
    public interface IMessage
    {
        /// <summary>
        /// Отправить сообщение пользователю
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SendMessageResult> SendMessage(Message message);
        /// <summary>
        /// Пришло новое сообщение от пользователя
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        Task<SendMessageResult> NewMessage(MessageDTO messageDTO);
    }
}