using Api.Data.Entity;
using Shared;

namespace Api.Interfaces.MessageRequests
{
    /// <summary>
    /// Методы с уведомлениями клиентов об изменении статусов
    /// </summary>
    public interface IMessageNotifyRequests
    {
        /// <summary>
        /// Отправить сообщением о новом пуше в системе всем, кроме получателя
        /// </summary>
        /// <param name="exceptConnectionId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendNotifyPush(string exceptConnectionId, MessageDTO message);
        /// <summary>
        /// Отправить пользователю данные о том, что сообщение прочитано
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task SendReadedNotify(MessageDTO message, User user);
        /// <summary>
        /// Отправить всем пользователям данные о том, что сообщение прочитано
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendReadedNotify(MessageDTO message);
    }
}