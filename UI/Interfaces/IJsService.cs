namespace UI.Interfaces
{
    /// <summary>
    /// Взаимодействие с JS
    /// </summary>
    public interface IJsService
    {
        /// <summary>
        /// Отправить алерт о новом сообщении
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fromLogin"></param>
        /// <returns></returns>
        Task SendAlert(string message, string fromLogin);
    }
}