using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using Assignment_3.Respository;
using Assignment_3.Service;
using Microsoft.EntityFrameworkCore;
using Assignment_3.Repositories;
using Assignment_3.Services;
using Assignment_3.ExtensionMethods;
using Assignment_3.Middleware;

namespace Assignment_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MySQLDBContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddServices();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
