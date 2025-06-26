namespace Shared.Results
{
    public class ResultBase
    {
        /// <summary>
        /// Текст ошибки (если есть)
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Успешен ли запрос
        /// </summary>
        public bool Success => ErrorMessage == null;
    }
}