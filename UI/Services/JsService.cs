using Microsoft.JSInterop;
using UI.Interfaces;

namespace UI.Services
{
    public class JsService : IJsService
    {
        private readonly IJSRuntime js;

        public JsService(IJSRuntime js)
        {
            this.js = js;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fromLogin"></param>
        /// <returns></returns>
        public async Task SendAlert(string message, string fromLogin)
        {
            await js.InvokeVoidAsync("NewAlert", message, fromLogin);
        }

        public async Task SendError(string text, string title)
        {
            await js.InvokeVoidAsync("NewError", text, title);
        }
    }
}