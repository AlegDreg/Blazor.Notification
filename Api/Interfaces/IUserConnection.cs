using Shared.Results;

namespace Api.Interfaces
{
    /// <summary>
    /// Управление подключениями пользователей
    /// </summary>
    public interface IUserConnection
    {
        /// <summary>
        /// Запрос на подключение и добавление пользователя
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>Модель пользователя, иначе, если кто-то уже подключен - null</returns>
        public Task<AuthResult> ConnectUser(string login, string connectionId);
        /// <summary>
        /// Пользователь отключается
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task DisconnectUser(string connectionId);
    }
}