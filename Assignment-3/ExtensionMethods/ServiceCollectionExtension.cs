using Assignment_3.Repositories;
using Assignment_3.Respository.IRepository;
using Assignment_3.Respository;
using Assignment_3.Service;
using Assignment_3.Services;

namespace Assignment_3.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRentalService, RentalService>();
            return services;
        }
    }
}
