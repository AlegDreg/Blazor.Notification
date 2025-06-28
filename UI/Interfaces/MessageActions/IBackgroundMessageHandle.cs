namespace UI.Interfaces.MessageActions
{
    /// <summary>
    /// Обработка сообщений с бэка
    /// </summary>
    public interface IBackgroundMessageHandle
    {
        /// <summary>
        /// Обработать сообщение с бэка
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Handle(string message);
    }
}