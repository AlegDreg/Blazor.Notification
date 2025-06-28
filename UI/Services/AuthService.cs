using UI.Interfaces;

namespace UI.Services
{
    public class AuthService : IAuthorize
    {
        private readonly IAuthRepository authRepository;
        private readonly IAuthRequests authRequest;

        public AuthService(IAuthRepository authRepository, IAuthRequests authRequest)
        {
            this.authRepository = authRepository;
            this.authRequest = authRequest;
        }

        public async Task<bool> IsAuthorized()
        {
            var userLogin = await authRepository.GetUserLogin();
            if (userLogin == null)
                return false;

            return await Authorize(userLogin) == null;
        }

        public async Task<string?> Authorize(string login)
        {
            var authResult = await authRequest.TryAuthorize(login);
            if (authResult != null && authResult.User != null)
            {
                return await authRepository.SetUserLogin(authResult.User.Login) ? null : "Ошибка сохранения авторизации";
            }
            else
            {
                await authRepository.SetUserLogin(null);
                return authResult?.ErrorMessage ?? "Ошибка авторизации";
            }
        }
    }
}