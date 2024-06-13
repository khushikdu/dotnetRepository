using Assignment_7.Interface.IService;
using Assignment_7.Service;
using Assignment_7.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Assignment_7.ExtensionMethod
{
    public static class ServiceProvider
    {
        public static IServiceProvider ConfigureServices()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IBlobService, BlobService>()
                .AddSingleton<Menu>();

            return services.BuildServiceProvider();
        }
    }
}
