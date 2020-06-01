namespace RMS.Services
{
    using API.Models.ResponseModels;
    using AutoMapper;
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Repositories.Contracts;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        /// Room-event service
        /// </summary>
        private readonly IRoomEventService roomEventService;

        /// <summary>
        /// Logger private field.
        /// </summary>
        private readonly ILogger<RoomService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomService"/> class.
        /// </summary>
        /// <param name="roomRepository">Room repository parameter.</param>
        /// <param name="roomEventService">Room event service parameter.</param>
        /// <param name="mapper">Auto mapper parameter.</param>
        /// <param name="logger">Logger parameter.</param>
        public RoomService(IRoomRepository roomRepository, IMapper mapper, IRoomEventService roomEventService, ILogger<RoomService> logger)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.roomEventService = roomEventService;
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

        /// <inheritdoc/>
        public async Task CreateRoomAsync(CreateRoomRequestModel createRoomRequestModel)
        {
            var newRoom = this.mapper.Map<Room>(createRoomRequestModel);

            var existingRoom = await this.roomRepository.FindAsync(predicate: r => r.Number == createRoomRequestModel.Number);

            if (existingRoom != null)
            {
                throw new InvalidOperationException($"Room with number {createRoomRequestModel.Number} already exists.");
            }

            await this.roomRepository.AddAsync(newRoom);

            await this.roomRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateRoomAsync(UpdateRoomRequestModel updateRoomRequestModel)
        {
            var dbRoom = await this.roomRepository.GetAsync(updateRoomRequestModel.Id);

            if (dbRoom == null)
            {
                throw new InvalidOperationException($"Room to update not found!");
            }

            this.mapper.Map<UpdateRoomRequestModel, Room>(updateRoomRequestModel, dbRoom);

            await this.roomRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteRoomAsync(Guid id)
        {
            var dbRoom = await this.roomRepository.GetAsync(id);

            if (dbRoom == null)
            {
                throw new InvalidOperationException("Invalid room to delete!");
            }

            await this.roomEventService.DeleteRoomsEventsByRoomIdAsync(id);
            await this.roomRepository.Delete(id);

            await this.roomRepository.SaveAsync();
        }

        public async Task<bool> GetRoomExistsByNumberAsync(string number)
        {
            var room = await this.roomRepository.FindAsync(predicate: r => r.Number == number);

            return room != null;
        }
    }
}