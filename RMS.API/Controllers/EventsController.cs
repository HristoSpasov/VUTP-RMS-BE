namespace RMS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using RMS.API.Models.Validators.Attributes;
    using RMS.Services.Contracts;

    /// <summary>
    /// Endpoints for register and managing events.
    /// </summary>
    public class EventsController : BaseController
    {
        private readonly IEventService eventService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsController"/> class.
        /// </summary>
        /// <param name="eventService">Event serveice parameter.</param>
        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns>Get all events data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<EventResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var events = await this.eventService.GetAllEventsAsync();

            return this.Ok(events);
        }

        /// <summary>
        /// Get filtered events events.
        /// </summary>
        /// <param name="eventFilterRequestModel">Event filter request model.</param>
        /// <returns>Get all filtered events data.</returns>
        [HttpGet]
        [Route("filter")]
        [ProducesResponseType(typeof(ICollection<EventResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFiltered([FromQuery] EventFilterRequestModel eventFilterRequestModel)
        {
            var events = await this.eventService.GetAllFilteredEventsAsync(eventFilterRequestModel);

            return this.Ok(events);
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        /// <param name="createEventRequest">Create event request model.</param>
        /// <returns>Create event result.</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateEvent(BaseEventRequestModel createEventRequest)
        {
            var result = await this.eventService.CreateEventAsync(createEventRequest);

            if (!result.Success)
            {
                return this.BadRequest(result.Errors);
            }

            return this.Ok("Event created.");
        }

        /// <summary>
        /// Update event.
        /// </summary>
        /// <param name="updateEventRequestModel">Update event request model.</param>
        /// <returns>Create Discipline result.</returns>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateEvent(BaseEventRequestModel updateEventRequestModel)
        {
            var result = await this.eventService.UpdateEventAsync(updateEventRequestModel);

            if (!result.Success)
            {
                return this.BadRequest(result.Errors);
            }

            return this.Ok("Event updated.");
        }

        /// <summary>
        /// Delete event.
        /// </summary>
        /// <param name="id">Event id to delete.</param>
        /// <returns>Delete teacher data result.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEvent([GuidNotEmpty] Guid id)
        {
            await this.eventService.DeleteEventAsync(id);

            return this.Ok("Event deleted successfully.");
        }
    }
}
