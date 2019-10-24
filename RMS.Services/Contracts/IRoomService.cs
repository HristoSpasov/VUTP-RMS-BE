namespace RMS.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Models.ResponseModels;

    public interface IRoomService
    {
        /// <summary>
        /// Get all specialties.
        /// </summary>
        /// <returns>List of all teachers.</returns>
        Task<ICollection<RoomResponseModel>> GetAllRoomsAsync();

        /// <summary>
        /// Get specialty by id.
        /// </summary>
        /// <param name="id">Teacher id parameter.</param>
        /// <returns>Teacher information.</returns>
        Task<RoomResponseModel> GetRoomByIdAsync(Guid id);
    }
}
