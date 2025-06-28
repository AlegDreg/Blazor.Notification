using Shared;

namespace Api.Interfaces.MessageRequests
{
    /// <summary>
    /// Методы для отправки пушей с новыми сообщениями
    /// </summary>
    public interface IMessageDeliveryRequests
    {
        /// <summary>
        /// Отправить пуш сообщение пользователю
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendPush(string connectionId, MessageDTO message);
    }
}