namespace UI.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> SetUserLogin(string? login);
        Task<string?> GetUserLogin();
        Task<bool> HasAuthorization();
    }
}