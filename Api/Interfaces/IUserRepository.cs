using Api.Data.Entity;

namespace Api.Interfaces
{
    /// <summary>
    /// Контракт для репозитория пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task<User> CreateUser(string login, string connectionId);
        /// <summary>
        /// Обнулить ConnectionId у пользователя, у которого указан <paramref name="connectionId"/>
        /// </summary>
        /// <param name="connectionId"></param>
        Task DisconnectUser(string connectionId);
        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<User?> GetUserByLogin(string login);
        /// <summary>
        /// Установить пользователю <paramref name="dbUser"/> айди подключения <paramref name="connectionId"/>
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task SetUserConnectionId(User dbUser, string connectionId);
        /// <summary>
        /// Удалить все айди подключений при запуске приложения
        /// </summary>
        /// <returns></returns>
        public Task ClearAllConnections();
    }
}