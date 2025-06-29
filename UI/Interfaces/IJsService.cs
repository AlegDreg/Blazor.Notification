namespace UI.Interfaces
{
    /// <summary>
    /// Взаимодействие с JS
    /// </summary>
    public interface IJsService
    {
        /// <summary>
        /// Отправить toast о новом сообщении
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fromLogin"></param>
        /// <returns></returns>
        Task SendAlert(string message, string fromLogin);
        /// <summary>
        /// Отправить toast об ошибке
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task SendError(string text,string title);
    }
}