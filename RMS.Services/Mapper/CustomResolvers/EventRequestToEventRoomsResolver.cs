namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRequestToEventRoomsResolver : IValueResolver<BaseEventRequestModel, Event, ICollection<RoomEvent>>
    {
        public ICollection<RoomEvent> Resolve(BaseEventRequestModel source, Event destination, ICollection<RoomEvent> destMember, ResolutionContext context)
        {
            var eventRooms = new List<RoomEvent>();

            source?.Rooms?.ToList()
                .ForEach(rId => 
                {
                    eventRooms.Add(new RoomEvent { RoomId = rId, Event = destination });
                });

            return eventRooms;
        }
    }
}
