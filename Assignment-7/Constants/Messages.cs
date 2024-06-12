using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7.Constants
{
    internal class Messages
    {
        public const string CreatedContainer = "Created container '{0}'";
        public const string ErrorCreatingContainer = "Error creating container '{0}': {1}";
        public const string GeneratedSampleFile = "Generated sample file '{0}'";
        public const string ErrorGeneratingSampleFile = "Error generating sample file: {0}";
        public const string UploadedBlob = "Uploaded blob '{0}' to container '{1}'";
        public const string ErrorUploadingBlob = "Error uploading blob '{0}': {1}";
        public const string ListingBlobs = "Listing blobs:";
        public const string BlobItemName = " - {0}";
        public const string ErrorListingBlobs = "Error listing blobs: {0}";
        public const string DownloadedBlob = "Downloaded blob to '{0}'";
        public const string ErrorDownloadingBlob = "Error downloading blob: {0}";
        public const string ErrorSettingAccessPolicy = "Error setting access policy for container '{0}': {1}";
        public const string CleanedUpFiles = "Cleaned up local files.";
        public const string ErrorCleaningUpFiles = "Error cleaning up files: {0}";
        public const string ContainerNameNotFound = "Container not found \nCreate a blob container first.";
        public const string InvalidChoice = "Invalid choice. Please try again.";
        public const string InvalidInput = "Invalid input. Please enter a number.";
    }
}
