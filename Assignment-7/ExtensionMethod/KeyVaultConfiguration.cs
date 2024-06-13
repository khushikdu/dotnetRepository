using Assignment_7.Constants;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7.ExtensionMethod
{
    public static class KeyVaultConfiguration
    {
        public static string? ConfigureKeyVault(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            try
            {
                string KeyVaultName = configuration["KEY_VAULT_NAME"];
                string keyVaultUrl = string.Format(AppConstants.KeyVaultURI, KeyVaultName);
                SecretClient secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

                KeyVaultSecret secret = secretClient.GetSecret(AppConstants.ConnectionString);

                string blobConnectionString = secret.Value;
                return blobConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Messages.SecretNotFound);
                throw;
            }
        }
    }
}
