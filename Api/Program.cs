using Api.Controllers;
using Api.Interfaces;
using Api.Interfaces.MessageRequests;
using Api.Services;
using Microsoft.EntityFrameworkCore;

/*
 
    ����� ��������� ��� �������

 */

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHostedService(services =>
            {
                return new BackgroundHostedNotifier(services);
            });

            builder.Services.AddSignalR();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app-db.sqlite"));
            builder.Services.AddScoped<IMessage, MessageService>();
            builder.Services.AddScoped<IMessageRepository, MessageDbRepository>();
            builder.Services.AddScoped<IUserConnection, UserConnectionService>();
            builder.Services.AddScoped<IUserRepository, UserDbRepository>();

            builder.Services.AddScoped<IMessageNotifyRequests, SignalrService>();
            builder.Services.AddScoped<IMessageDeliveryRequests, SignalrService>();

            builder.Services.AddScoped<IBackgroundMessages, BackgroundMessageService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.MapGet("/test", () => "ok");

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<MessageHub>("/hub");

            var scoppe = app.Services.CreateScope();
            try
            {
                await scoppe.ServiceProvider.GetRequiredService<IUserRepository>().ClearAllConnections();

            }
            finally
            {
                scoppe.Dispose();
            }

            app.Run();
        }
    }
}