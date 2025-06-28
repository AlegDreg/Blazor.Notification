using Shared.Results;

namespace UI.Interfaces
{
    /// <summary>
    /// Методы для работы с запросами на авторизацию
    /// </summary>
    public interface IAuthRequests
    {
        /// <summary>
        /// Попытаться авторизоваться
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<AuthResult?> TryAuthorize(string login);
    }
}