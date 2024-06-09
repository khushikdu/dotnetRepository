using Assignment_5.Interface.IService;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Assignment_5.Service
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(IConfiguration configuration)
        {
            var vaultName = configuration["AzureKeyVault:VaultName"];
            var kvUri = $"https://{vaultName}.vault.azure.net/";

            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        public async Task<string> CreateSecretAsync(string name, string value)
        {
            try
            {
                var response = await _secretClient.SetSecretAsync(name, value);
                return $"Secret '{response.Value.Name}' created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating secret: {ex.Message}";
            }
        }

        public async Task<string> RetrieveSecretAsync(string name)
        {
            try
            {
                var response = await _secretClient.GetSecretAsync(name);
                return response.Value.Value;
            }
            catch (Exception ex)
            {
                return $"Error retrieving secret: {ex.Message}";
            }
        }

        public async Task<string> DeleteSecretAsync(string name, int? waitDurationSeconds = null)
        {
            try
            {
                var response = await _secretClient.StartDeleteSecretAsync(name);
                if (waitDurationSeconds.HasValue)
                {
                    await Task.Delay(waitDurationSeconds.Value * 1000);
                    await response.WaitForCompletionAsync();
                }
                return $"Secret '{name}' deleted successfully.";
            }
            catch (Exception ex)
            {
                return $"Error deleting secret: {ex.Message}";
            }
        }

        public async Task<string> PurgeSecretAsync(string name)
        {
            try
            {
                await _secretClient.PurgeDeletedSecretAsync(name);
                return $"Secret '{name}' purged successfully.";
            }
            catch (Exception ex)
            {
                return $"Error purging secret: {ex.Message}";
            }
        }
    }
}
