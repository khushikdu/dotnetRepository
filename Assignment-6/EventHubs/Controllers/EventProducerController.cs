using EventHubs.Constants;
using EventHubs.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace EventHubs.Controllers
{
    [ApiController]
    [Route(RoutePath.EventProducerController)]
    public class EventProducerController : ControllerBase
    {
        private readonly IEventProducerService _eventProducerService;

        public EventProducerController(IEventProducerService eventProducerService)
        {
            _eventProducerService = eventProducerService;
        }

        [HttpPost(RoutePath.Produce)]
        public async Task<IActionResult> ProduceEvent([FromBody] string eventData)
        {
            try
            {
                await _eventProducerService.ProduceEventAsync(eventData);
                return Ok(Messages.EventProduced);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{Messages.ProduceEventFailed}: {ex.Message}");
            }
        }
    }
}
