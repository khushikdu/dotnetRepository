using Assignment_7.ExtensionMethod;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Assignment_7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ExtensionMethod.ServiceProvider.ConfigureServices();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var keyVaultUrl = configuration["AzureKeyVault:VaultUri"];
            var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

            KeyVaultSecret secret = await secretClient.GetSecretAsync("BlobConnectionString");
            var blobConnectionString = secret.Value;
            configuration["BlobConnectionString"]= blobConnectionString;

            Console.WriteLine($"Retrieved Blob Connection String: {blobConnectionString}");
            await BlobOperations.RunAsync(serviceProvider);
        }
    }
}
