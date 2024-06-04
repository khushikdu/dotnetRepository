using Assessment_1.Interfaces.IRepository;
using Assessment_1.Interfaces.IService;
using Assessment_1.Repository;
using Assessment_1.Service;
using Assignment_2.Repository;
using Assignment_2.Repository.Interface;
using Assignment_2.Services.Interface;

namespace Assignment_3.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRiderRepository, RiderRepository>();
            services.AddScoped<IRiderService, RiderService>();

            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDriverService, DriverService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
