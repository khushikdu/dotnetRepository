using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7.Constants
{
    public static class AppConstants
    {
        public const string ConnectionString = "BlobConnectionString";
        public const string KeyVaultName = "KEY_VAULT_NAME";
        public const string KeyVaultURI = "https://{0}.vault.azure.net/";

        public const string DownloadPrefix = "downloaded-";

        public const string DirectoryPath = "C:\\Users\\HP\\Desktop\\Homeworks";
        public const string FileName = "sample.txt";
    }
}
