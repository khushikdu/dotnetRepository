using Assignment_5.Constants;
using Assignment_5.Interface.IService;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading.Tasks;

namespace Assignment_5.Service
{
    /// <summary>
    /// Service for interacting with Azure Key Vault.
    /// </summary>
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;
        private readonly bool _softDeleteEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public KeyVaultService(IConfiguration configuration)
        {
            string? vaultName = Environment.GetEnvironmentVariable(KeyVaultConstants.KeyVaultName);
            if (string.IsNullOrEmpty(vaultName))
            {
                throw new ArgumentNullException(Messages.EnvVariableNotSet);
            }
            string kvUri = $"https://{vaultName}.vault.azure.net/";

            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            _softDeleteEnabled = bool.Parse(configuration[KeyVaultConstants.SoftDeleteEnabled] ?? "true");
        }

        /// <summary>
        /// Creates a secret with the specified name and value in the Key Vault.
        /// </summary>
        /// <param name="name">The name of the secret.</param>
        /// <param name="value">The value of the secret.</param>
        /// <returns>A task representing the asynchronous operation, containing the result message.</returns>
        public async Task<string> CreateSecretAsync(string name, string value)
        {
            try
            {
                KeyVaultSecret response = await _secretClient.SetSecretAsync(name, value);
                return string.Format(Messages.CreateSecretSuccess, response.Name);
            }
            catch (Exception ex)
            {
                return string.Format(Messages.CreateSecretError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the value of the secret with the specified name from the Key Vault.
        /// </summary>
        /// <param name="name">The name of the secret to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the secret value.</returns>
        public async Task<string> RetrieveSecretAsync(string name)
        {
            try
            {
                KeyVaultSecret response = await _secretClient.GetSecretAsync(name);
                return response.Value;
            }
            catch (Exception ex)
            {
                return string.Format(Messages.RetrieveSecretError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the secret with the specified name from the Key Vault.
        /// </summary>
        /// <param name="name">The name of the secret to delete.</param>
        /// <param name="waitDurationSeconds">Optional. The duration to wait before purging the deleted secret, in seconds.</param>
        /// <returns>A task representing the asynchronous operation, containing the result message.</returns>
        public async Task<string> DeleteSecretAsync(string name, int? waitDurationSeconds = null)
        {
            try
            {
                DeleteSecretOperation response = await _secretClient.StartDeleteSecretAsync(name);
                if (waitDurationSeconds.HasValue)
                {
                    await Task.Delay(waitDurationSeconds.Value * 1000);
                    await response.WaitForCompletionAsync();
                }
                return string.Format(Messages.DeleteSecretSuccess, name);
            }
            catch (Exception ex)
            {
                return string.Format(Messages.DeleteSecretError, ex.Message);
            }
        }

        /// <summary>
        /// Purges the deleted secret with the specified name from the Key Vault.
        /// </summary>
        /// <param name="name">The name of the deleted secret to purge.</param>
        /// <returns>A task representing the asynchronous operation, containing the result message.</returns>
        public async Task<string> PurgeSecretAsync(string name)
        {
            try
            {
                if (!_softDeleteEnabled)
                {
                    return Messages.SoftDeleteNotEnabled;
                }
                await _secretClient.PurgeDeletedSecretAsync(name);
                return string.Format(Messages.PurgeSecretSuccess, name);
            }
            catch (Exception ex)
            {
                return string.Format(Messages.PurgeSecretError, ex.Message);
            }
        }
    }
}
