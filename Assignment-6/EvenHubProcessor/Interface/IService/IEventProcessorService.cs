namespace EvenHubProcessor.Interface.IService
{
    public interface IEventProcessorService
    {
        Task StartProcessingAsync();
        Task StopProcessingAsync();
    }
}
