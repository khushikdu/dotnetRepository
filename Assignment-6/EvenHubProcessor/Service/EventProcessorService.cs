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
        private readonly ILogger<EventProcessorService> _logger;


        public EventProcessorService(IConfiguration configuration, ILogger<EventProcessorService> logger)
        {
            _logger = logger;
            string connectionString = configuration["EventHub:ConnectionString"];
            string eventHubName = configuration["EventHub:EventHubName"];
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            string blobStorageConnectionString = configuration["BlobStorage:ConnectionString"];
            string blobContainerName = configuration["BlobStorage:ContainerName"];

            // Initialize BlobContainerClient and EventProcessorClient
            _storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            _processorClient = new EventProcessorClient(_storageClient, consumerGroup, connectionString, eventHubName);

            // Register event handlers
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

        /// <summary>
        /// Handles incoming events from the Event Hub.
        /// </summary>
        /// <param name="eventArgs">The event arguments.</param>
        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {

            string eventData = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            _logger.LogInformation($"Received event: {eventData}");

            // Update checkpoint in the blob storage
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        /// <summary>
        /// Handles errors that occur during event processing.
        /// </summary>
        /// <param name="eventArgs">The error event arguments.</param>
        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            _logger.LogInformation($"Error processing event: {eventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
        
    }
}
