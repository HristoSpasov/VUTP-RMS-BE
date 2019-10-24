namespace RMS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Models.ResponseModels;
    using AutoMapper;
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Repositories.Contracts;

    public class RoomService : IRoomService
    {
        /// <summary>
        /// Room repository private field.
        /// </summary>
        private readonly IRoomRepository roomRepository;

        /// <summary>
        /// Auto mapper private field.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Logger private field.
        /// </summary>
        private readonly ILogger<RoomService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomService"/> class. 
        /// </summary>
        /// <param name="roomRepository">Room repository parameter.</param>
        /// <param name="mapper">Auto mapper parameter.</param>
        /// <param name="logger">Logger parameter.</param>
        public RoomService(IRoomRepository roomRepository, IMapper mapper, ILogger<RoomService> logger)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomResponseModel>> GetAllRoomsAsync()
        {
            var rooms = await this.roomRepository.GetAllAsync(enableTracking: false);
            var mappedRooms = this.mapper.Map<ICollection<RoomResponseModel>>(rooms);

            return mappedRooms;
        }

        /// <inheritdoc/>
        public async Task<RoomResponseModel> GetRoomByIdAsync(Guid id)
        {
            var room = await this.roomRepository.GetAsync(id: id, enableTracking: false);

            if (room == null)
            {
                this.logger.LogError($"Room with id '{id}' was not found in database.");
                throw new InvalidOperationException("Room not found!");
            }

            var mappedRoom = this.mapper.Map<RoomResponseModel>(room);

            return mappedRoom;
        }
    }
}
