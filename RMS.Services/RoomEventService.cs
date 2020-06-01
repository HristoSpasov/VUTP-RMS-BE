namespace RMS.Services
{
    using Microsoft.EntityFrameworkCore;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoomEventService : IRoomEventService
    {
        private readonly IRoomEventRepository roomEventRepository;
        private readonly IEventService eventService;

        public RoomEventService(IRoomEventRepository roomEventRepository, IEventService eventService)
        {
            this.roomEventRepository = roomEventRepository;
            this.eventService = eventService;
        }

        public async Task DeleteRoomsEventsByRoomIdAsync(Guid roomId)
        {
            // Check if this is the only room assigned to the relating events
            var eventsByRoom = await this.eventService.GetEventsByRoomIdAsync(roomId);

            foreach (var ev in eventsByRoom)
            {
                if (ev.Rooms.Count <= 1)
                {
                    // This is the only room for this event
                    // Delete the event
                    await this.eventService.DeleteEventAsync(ev.Id);
                }
                else
                {
                    // There are other rooms for this event, so the event is preserved
                    // Delete only the records for the current room
                    var roomEvents = await this.roomEventRepository.FindAllAsync(predicate: r => r.RoomId == roomId);

                    roomEvents.ToList().ForEach(e =>
                    {
                        e.IsDeleted = true;
                    });

                    await this.roomEventRepository.SaveAsync();
                }
            }
        }
    }
}
