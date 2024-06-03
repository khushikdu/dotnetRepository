using Homework_7.Middleware;
using Homework_7.Service.Interface;
using Homework_7.Service;

namespace Homework_7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            //using AddSingleton as the userlist is present in the service layer
            builder.Services.AddSingleton<IUserRegistrationService, UserRegistrationService>();

            builder.Services.AddTransient<ErrorHandlingIMiddleware>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            //used two error handling middlewares to show the implementation using the IMiddleware interface.
            app.UseMiddleware<ErrorHandlingIMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
