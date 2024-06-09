namespace Assignment_5.Interface.IService
{
    public interface IKeyVaultService
    {
        Task<string> CreateSecretAsync(string name, string value);
        Task<string> RetrieveSecretAsync(string name);
        Task<string> DeleteSecretAsync(string name, int? waitDurationSeconds = null);
        Task<string> PurgeSecretAsync(string name);
    }
}
