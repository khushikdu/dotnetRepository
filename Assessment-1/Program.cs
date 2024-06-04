using Assessment_1.DBContext;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.Interfaces.IService;
using Assessment_1.Repository;
using Assessment_1.Service;
using Assignment_2.ExtensionMethods;
using Assignment_2.Middlewares;
using Assignment_3.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TaxiServiceDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddServices();
            builder.Services.AddJwtAuthentication(builder.Configuration);

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

            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
