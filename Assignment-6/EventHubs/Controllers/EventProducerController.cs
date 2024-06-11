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

        /// <summary>
        /// Produces an event to the Event Hub.
        /// </summary>
        /// <param name="eventData">The event data to produce.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
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
