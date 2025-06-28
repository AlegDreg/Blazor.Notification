using Shared;

namespace UI.Interfaces.MessageRequests
{
    /// <summary>
    /// Отправка и отслеживание статусов сообщений
    /// </summary>
    public interface IMessageStateRequests
    {
        /// <summary>
        /// Отправить данные о том, что сообщение прочли
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendReadedMessage(MessageDTO message);
    }
}