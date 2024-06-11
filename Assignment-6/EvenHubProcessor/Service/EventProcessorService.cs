using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using EvenHubProcessor.Interface.IService;
using Microsoft.Extensions.Configuration;
using System.Text;
using Azure.Storage.Blobs;

namespace EvenHubProcessor.Service
{
    public class EventProcessorService : IEventProcessorService
    {
        private readonly EventProcessorClient _processorClient;
        private readonly BlobContainerClient _storageClient;

        public EventProcessorService(IConfiguration configuration)
        {
            string connectionString = configuration["EventHub:ConnectionString"];
            string eventHubName = configuration["EventHub:EventHubName"];
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            string blobStorageConnectionString = configuration["BlobStorage:ConnectionString"];
            string blobContainerName = configuration["BlobStorage:ContainerName"];

            _storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            _processorClient = new EventProcessorClient(_storageClient, consumerGroup, connectionString, eventHubName);

            _processorClient.ProcessEventAsync += ProcessEventHandler;
            _processorClient.ProcessErrorAsync += ProcessErrorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _processorClient.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            await _processorClient.StopProcessingAsync();
        }

        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {

            string eventData = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            Console.WriteLine($"Received event: {eventData}");

            // Update checkpoint in the blob storage
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"Error processing event: {eventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
