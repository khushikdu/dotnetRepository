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
        private const int _maxRetryAttemps = 3;

        public EventProducerService(IConfiguration configuration)
        {
            string connectionString = configuration["EventHub:ConnectionString"];
            string eventHubName = configuration["EventHub:EventHubName"];
            _producerClient = new EventHubProducerClient(connectionString, eventHubName);
        }

        public async Task ProduceEventAsync(string eventData)
        {
            for (int retryAttempt = 0; retryAttempt < _maxRetryAttemps; retryAttempt++)
            {
                try
                {
                    using EventDataBatch eventBatch = await _producerClient.CreateBatchAsync();
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventData)));
                    await _producerClient.SendAsync(eventBatch);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            throw new Exception(Messages.ProduceEventRetryFailed);
        }
    }
}
