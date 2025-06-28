namespace Api.Interfaces.MessageRequests
{
    /// <summary>
    /// Интерфейс для фоновой задачи
    /// </summary>
    public interface IBackgroundMessages
    {
        /// <summary>
        /// Отправить всем подключенным пользователям сообщение из фоновой задачи
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageToAll(string message);
    }
}