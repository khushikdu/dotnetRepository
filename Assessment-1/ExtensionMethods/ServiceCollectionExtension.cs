using Assessment_1.Interfaces.IService;
using Assessment_1.Services;

namespace Assignment_3.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IRiderService, RiderService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IRideService, RideService>();
            return services;
        }
    }
}
