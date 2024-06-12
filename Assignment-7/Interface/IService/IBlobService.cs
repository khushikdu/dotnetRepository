using Azure;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7.Interface.IService
{
    public interface IBlobService
    {
        Task CreateContainerAsync();
        string GenerateSampleFile(string directoryPath, string fileName);
        Task UploadBlobAsync(string filePath);
        Task ListBlobsAsync();
        Task DownloadBlobAsync(string originalFilePath);
        Task SetContainerAccessPolicyAsync();
        void CleanUp(string filePath);
    }
}
