using Shared.Results;

namespace UI.Interfaces.Messages
{
    /// <summary>
    /// Управление отправкой сообщений
    /// </summary>
    public interface IMessageDelivery
    {
        /// <summary>
        /// Отправить сообщение пользователю
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toLogin"></param>
        /// <returns></returns>
        Task<SendMessageResult?> SendMessage(string message, string toLogin);
    }
}