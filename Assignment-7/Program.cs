using Assignment_7.Constants;
using Assignment_7.ExtensionMethod;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                IServiceProvider serviceProvider = ExtensionMethod.ServiceProvider.ConfigureServices();
                IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

                string? blobConnectionString = KeyVaultConfiguration.ConfigureKeyVault(serviceProvider, configuration);

                configuration[AppConstants.ConnectionString] = blobConnectionString;
                await BlobOperations.PerformBlobOperationsAsync(serviceProvider);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(Messages.TryLater);
            }
        }
    }
}
