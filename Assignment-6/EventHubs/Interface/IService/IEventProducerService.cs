namespace EventHubs.Interface.IService
{
    public interface IEventProducerService
    {
        Task ProduceEventAsync(string eventData);
    }
}
