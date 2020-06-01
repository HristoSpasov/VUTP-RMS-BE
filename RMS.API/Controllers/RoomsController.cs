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
    using RMS.API.Models.RequestModels;
    using Services.Contracts;

    /// <summary>
    /// Endpoints for consuming and managing room entities.
    /// </summary>
    public class RoomsController : BaseController
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
        public async Task<IActionResult> GetRoomById([FromRoute][GuidNotEmpty] Guid id)
        {
            var room = await this.roomService.GetRoomByIdAsync(id);

            return this.Ok(room);
        }

        /// <summary>
        /// Validate room number endpoint.
        /// </summary>
        /// <param name="number">Room number to validate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Route("validateRoomNumber/{number}")]
        public async Task<IActionResult> ValidateRoomNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return this.BadRequest("Room number is required!");
            }

            if (number.Length < 1 || number.Length > 20)
            {
                return this.BadRequest("Room number lenght should be between 1 and 20 characters.");
            }

            var roomExists = await this.roomService.GetRoomExistsByNumberAsync(number);

            if (roomExists)
            {
                return this.BadRequest($"Room with number {number} already exists.");
            }

            return new OkObjectResult($"Room number {number} is valid.");
        }

        /// <summary>
        /// Create new room.
        /// </summary>
        /// <param name="createRoomRequestModel">Create teacher request model.</param>
        /// <returns>Create room data resukt.</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateRoom(CreateRoomRequestModel createRoomRequestModel)
        {
            await this.roomService.CreateRoomAsync(createRoomRequestModel);

            return this.Ok($"Room {createRoomRequestModel.Number} successfully created.");
        }

        /// <summary>
        /// Update room.
        /// </summary>
        /// <param name="updateRoomRequestModel">Update room request model.</param>
        /// <returns>Uodate room data result.</returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomRequestModel updateRoomRequestModel)
        {
            await this.roomService.UpdateRoomAsync(updateRoomRequestModel);

            return this.Ok($"Room number changed from <number> to {updateRoomRequestModel.Number}");
        }

        /// <summary>
        /// Delete room.
        /// </summary>
        /// <param name="id">Room id to delete.</param>
        /// <returns>Delete room result.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTeacher([GuidNotEmpty] Guid id)
        {
            await this.roomService.DeleteRoomAsync(id);

            return this.Ok($"Room <number> successfully deleted!");
        }
    }
}