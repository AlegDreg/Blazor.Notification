namespace Shared.Results
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthResult : ResultBase
    {
        public UserDTO? User { get; set; }
        /// <summary>
        /// Авторизация с ошибкой
        /// </summary>
        /// <param name="errorMessage"></param>
        public AuthResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Авторизация успешна
        /// </summary>
        /// <param name="user"></param>
        public AuthResult(UserDTO user)
        {
            User = user;
        }
    }
}