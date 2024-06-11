using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubs.Constants;
using EventHubs.Interface.IService;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Text;

namespace EventHubs.Service
{
    public class EventProducerService : IEventProducerService
    {
        private readonly EventHubProducerClient _producerClient;
        private readonly ILogger<EventProducerService> _logger;
        private const int _maxRetryAttemps = 3;

        public EventProducerService(IConfiguration configuration, ILogger<EventProducerService> logger)
        {
            _logger = logger;
            string connectionString = configuration["EventHub:ConnectionString"];
            string eventHubName = configuration["EventHub:EventHubName"]; 
            _producerClient = new EventHubProducerClient(connectionString, eventHubName);
        }

        /// <summary>
        /// Produces an event to the Event Hub with retry logic.
        /// </summary>
        /// <param name="eventData">The event data to produce.</param>
        public async Task ProduceEventAsync(string eventData)
        {
            for (int retryAttempt = 0; retryAttempt < _maxRetryAttemps; retryAttempt++)
            {
                try
                {
                    // Create a batch of events to send to the Event Hub
                    using EventDataBatch eventBatch = await _producerClient.CreateBatchAsync();
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventData)));
                    await _producerClient.SendAsync(eventBatch);
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            // Throw an exception if the maximum number of retry attempts is reached
            throw new Exception(Messages.ProduceEventRetryFailed);
        }
        
    }
}
