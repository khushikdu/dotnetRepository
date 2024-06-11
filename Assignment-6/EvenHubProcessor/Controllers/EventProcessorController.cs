using EvenHubProcessor.Constants;
using EvenHubProcessor.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace EvenHubProcessor.Controllers
{
    [ApiController]
    [Route(RoutePaths.EventProcessorController)]
    public class EventProcessorController : ControllerBase
    {
        private readonly IEventProcessorService _eventProcessorService;

        public EventProcessorController(IEventProcessorService eventProcessorService)
        {
            _eventProcessorService = eventProcessorService;
        }

        /// <summary>
        /// Starts the event processing.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost(RoutePaths.Start)]
        public async Task<IActionResult> StartProcessing()
        {
            try
            {
                await _eventProcessorService.StartProcessingAsync();
                return Ok(Messages.EventProcessingStarted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{Messages.StartProcessingFailed}: {ex.Message}");
            }
        }

        /// <summary>
        /// Stops the event processing.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost(RoutePaths.Stop)]
        public async Task<IActionResult> StopProcessing()
        {
            try
            {
                await _eventProcessorService.StopProcessingAsync();
                return Ok(Messages.EventProcessingStopped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{Messages.StopProcessingFailed}: {ex.Message}");
            }
        }
    }
}