using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Radzen;
using System.Net.Http.Json;
using UI;
using UI.Data;
using UI.Interfaces;
using UI.Interfaces.MessageActions;
using UI.Interfaces.MessageRequests;
using UI.Interfaces.Messages;
using UI.Services;
using UI.Services.MessageActions;

/*

    Адрес бэка указан в appsettings.json

 */

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

var appSettings = await http.GetFromJsonAsync<AppSettings>("appsettings.json");

if (appSettings == null)
    throw new ArgumentNullException(nameof(appSettings));

builder.Services.AddSingleton(appSettings);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IJsService, JsService>();
builder.Services.AddSingleton<IWebRequest, WebRequestService>();
builder.Services.AddSingleton<IAppInitializer, AppInitializer>();

builder.Services.AddScoped<IAuthorize, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepositoryService>();

builder.Services.AddScoped<IAuthRequests, WebRequestService>();
builder.Services.AddScoped<IMessageSendRequests, WebRequestService>();
builder.Services.AddScoped<IMessageStateRequests, WebRequestService>();
builder.Services.AddScoped<IMessageRecieveRequests, WebRequestService>();

builder.Services.AddScoped<IMessageDelivery, MessageDeliveryService>();
builder.Services.AddScoped<IMessageRecieve, MessageRecieveService>();
builder.Services.AddSingleton<IMessageTracking, MessageStateService>();

builder.Services.AddSingleton<BackgroundMessageRouter>();
builder.Services.AddSingleton<IBackgroundMessageHandle, BackgroundMessageAlert>();

builder.Services.AddSingleton(sp =>
{
    return new HubConnectionBuilder()
    .WithUrl(appSettings.ApiUrl + "/hub")
    .WithAutomaticReconnect()
    .Build();
});

await builder.Build().RunAsync();