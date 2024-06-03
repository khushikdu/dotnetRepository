using Assignment_2.Repository;
using Assignment_2.Repository.Interface;
using Assignment_2.Services;
using Assignment_2.Services.Interface;

namespace Assignment_2.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
