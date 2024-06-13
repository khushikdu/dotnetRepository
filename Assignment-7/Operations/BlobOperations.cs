using Assignment_7.Constants;
using Assignment_7.Interface.IService;
using Assignment_7.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_7
{
    public static class BlobOperations
    {
        public static async Task PerformBlobOperationsAsync(IServiceProvider serviceProvider)
        {
            IBlobService blobService = serviceProvider.GetService<IBlobService>();
            Menu menu = serviceProvider.GetService<Menu>();
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

            bool exit = false;

            while (!exit)
            {
                menu.ShowMainScreenOptions();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                        {
                            case 1:
                                await blobService.CreateContainerAsync();
                                break;
                            case 2:
                                await blobService.SetContainerAccessPolicyAsync();
                                break;
                            case 3:
                                string filePath = blobService.GenerateSampleFile(AppConstants.DirectoryPath, AppConstants.FileName);
                                await blobService.UploadBlobAsync(filePath);
                                break;
                            case 4:
                                await blobService.ListBlobsAsync();
                                break;
                            case 5:
                                await blobService.DownloadBlobAsync(AppConstants.FileName);
                                break;
                            case 6:
                                blobService.CleanUp(AppConstants.FileName);
                                break;
                            case 7:
                                exit = true;
                                break;
                            default:
                                Console.WriteLine(Messages.InvalidChoice);
                                break;
                        }
                }
                else
                {
                    Console.WriteLine(Messages.InvalidInput);
                }
            }
        }
    }
}
