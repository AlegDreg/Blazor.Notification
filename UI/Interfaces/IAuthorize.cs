namespace UI.Interfaces
{
    public interface IAuthorize
    {
        /// <summary>
        /// Авторизован ли пользователь
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAuthorized();
        /// <summary>
        /// Выполнить авторизацию
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Текст ошибки, если есть</returns>
        Task<string?> Authorize(string login);
    }
}
