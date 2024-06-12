using Assignment_7.ExtensionMethod;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Assignment_7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //BlobController blobController = new BlobController();
            var serviceProvider = ExtensionMethod.ServiceProvider.ConfigureServices();
            await BlobOperations.RunAsync(serviceProvider);
        }
    }
}
