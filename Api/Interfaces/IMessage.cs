using Shared;
using Shared.Results;

namespace Api.Interfaces
{
    public interface IMessage
    {
        /// <summary>
        /// Получатель прочитал сообщение, оповещаем об этом обоих пользователей
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        Task MessageReaded(MessageDTO messageDTO);
        /// <summary>
        /// Пришло новое сообщение от пользователя
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        Task<SendMessageResult> NewMessage(NewMessageDTO messageDTO);
    }
}