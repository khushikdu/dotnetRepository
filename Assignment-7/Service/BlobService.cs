using Assignment_7.Constants;
using Assignment_7.Interface.IService;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Assignment_7.Service
{
    internal class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _containerClient;

        private string _containerName;
        private readonly string _directoryPath;
        private readonly string _fileName;

        public BlobService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["BlobConnectionString"]);
            _directoryPath = configuration["DirectoryPath"];
            _fileName = configuration["FileName"];
        }

        public async Task CreateContainerAsync()
        {
            try
            {
                _containerName = $"blob-container-{Guid.NewGuid()}";
                _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await _containerClient.CreateAsync();
                Console.WriteLine(string.Format(Messages.CreatedContainer, _containerName));
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorCreatingContainer, _containerName, ex.Message));
            }
        }

        public string GenerateSampleFile(string directoryPath, string fileName)
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, fileName);
                File.WriteAllText(filePath, "This is a sample text file for Azure Blob Storage operations.");
                Console.WriteLine(string.Format(Messages.GeneratedSampleFile, filePath));
                return filePath;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorGeneratingSampleFile, ex.Message));
                return "";
            }
        }

        public async Task UploadBlobAsync(string filePath)
        {
            try
            {
                if (!ContainerExistsAsync()) return;
                BlobClient blobClient = _containerClient.GetBlobClient(Path.GetFileName(filePath));
                await using FileStream uploadFileStream = File.OpenRead(filePath);
                await blobClient.UploadAsync(uploadFileStream, true);
                Console.WriteLine(string.Format(Messages.UploadedBlob, filePath, _containerName));
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorUploadingBlob, filePath, ex.Message));
            }
        }

        public async Task ListBlobsAsync()
        {
            try
            {
                if (!ContainerExistsAsync()) return;
                Console.WriteLine(Messages.ListingBlobs);
                await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
                {
                    Console.WriteLine(string.Format(Messages.BlobItemName, blobItem.Name));
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorListingBlobs, ex.Message));
            }
        }

        public async Task DownloadBlobAsync(string originalFilePath)
        {
            try
            {
                if (!ContainerExistsAsync()) return;
                BlobClient blobClient = _containerClient.GetBlobClient(Path.GetFileName(originalFilePath));
                string downloadFilePath = Path.Combine(_directoryPath, "downloaded-" + Path.GetFileName(originalFilePath));
                await blobClient.DownloadToAsync(downloadFilePath);
                Console.WriteLine(string.Format(Messages.DownloadedBlob, downloadFilePath));
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorDownloadingBlob, ex.Message));
            }
        }

        public async Task SetContainerAccessPolicyAsync()
        {
            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorSettingAccessPolicy, _containerName, ex.Message));
            }
        }

        public void CleanUp(string filePath)
        {
            try
            {
                if (!ContainerExistsAsync()) return;
                _containerClient.DeleteAsync();
                File.Delete(Path.Combine(_directoryPath, Path.GetFileName(filePath)));
                File.Delete(Path.Combine(_directoryPath, "downloaded-" + Path.GetFileName(filePath)));
                Console.WriteLine(Messages.CleanedUpFiles);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(string.Format(Messages.ErrorCleaningUpFiles, ex.Message));
            }
        }

        private bool ContainerExistsAsync()
        {
            if (_containerName == null)
            {
                Console.WriteLine(Messages.ContainerNameNotFound);
                return false;
            }
            return true;
        }
    }
}
