namespace RMS.Services.Contracts
{
    using API.Models.ResponseModels;
    using RMS.API.Models.RequestModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomService
    {
        /// <summary>
        /// Get all rooms.
        /// </summary>
        /// <returns>List of all rooms.</returns>
        Task<ICollection<RoomResponseModel>> GetAllRoomsAsync();

        /// <summary>
        /// Get room by id.
        /// </summary>
        /// <param name="id">Room id parameter.</param>
        /// <returns>Room information.</returns>
        Task<RoomResponseModel> GetRoomByIdAsync(Guid id);

        /// <summary>
        /// Get room exists by number.
        /// </summary>
        /// <param name="number">Room number parameter.</param>
        /// <returns>Room exists.</returns>
        Task<bool> GetRoomExistsByNumberAsync(string number);

        /// <summary>
        /// Create new room.
        /// </summary>
        /// <param name="createRoomRequestModel">Create new room request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateRoomAsync(CreateRoomRequestModel createRoomRequestModel);

        /// <summary>
        /// Update room data.
        /// </summary>
        /// <param name="updateRoomRequestModel">Update room request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateRoomAsync(UpdateRoomRequestModel updateTeacherRequestModel);

        /// <summary>
        /// Delete room.
        /// </summary>
        /// <param name="id">Room id to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteRoomAsync(Guid id);
    }
}