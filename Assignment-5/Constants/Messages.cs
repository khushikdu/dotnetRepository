namespace Assignment_5.Constants
{
    public static class Messages
    {
        // Error Messages
        public static readonly string CreateSecretError = "Error creating secret: {0}";
        public static readonly string RetrieveSecretError = "Error retrieving secret: {0}";
        public static readonly string DeleteSecretError = "Error deleting secret: {0}";
        public static readonly string PurgeSecretError = "Error purging secret: {0}";
        public static readonly string SoftDeleteNotEnabled = "Error purging secret. Soft delete not enables";
        public static readonly string EnvVariableNotSet = "KEY_VAULT_NAME environment variable is not set.";

        // Success Messages
        public static readonly string CreateSecretSuccess = "Secret '{0}' created successfully.";
        public static readonly string DeleteSecretSuccess = "Secret '{0}' deleted successfully.";
        public static readonly string PurgeSecretSuccess = "Secret '{0}' purged successfully.";
    }
}
