namespace RMS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.ResponseModels;
    using Models.Validators.Attributes;
    using Services.Contracts;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> logger;
        private readonly IRoomService roomService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="logger">Logger parameter.</param>
        /// <param name="roomService">Room service parameter.</param>
        public RoomsController(ILogger<RoomsController> logger, IRoomService roomService)
        {
            this.logger = logger;
            this.roomService = roomService;
        }

        /// <summary>
        /// Get all rooms.
        /// </summary>
        /// <returns>Get all rooms data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RoomResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await this.roomService.GetAllRoomsAsync();

            return this.Ok(rooms);
        }

        /// <summary>
        /// Get room by id.
        /// </summary>
        /// <param name="id">Room id.</param>
        /// <returns>Get a room data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(RoomResponseModel), StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IActionResult> GetRoomById([FromRoute] [GuidNotEmpty] Guid id)
        {
            try
            {
                var room = await this.roomService.GetRoomByIdAsync(id);

                return this.Ok(room);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
