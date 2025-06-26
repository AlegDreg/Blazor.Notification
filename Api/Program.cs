using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;

/*
 
    Нужно запустить оба проекта

 */

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app-db.sqlite"));

            builder.Services.AddScoped<IMessage, MessageService>();
            builder.Services.AddScoped<IMessageRepository, MessageDbRepository>();
            builder.Services.AddScoped<IUserConnection, UserConnectionService>();
            builder.Services.AddScoped<IUserRepository,UserDbRepository>();
            builder.Services.AddScoped<ISignalrRequest, SignalrService>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}