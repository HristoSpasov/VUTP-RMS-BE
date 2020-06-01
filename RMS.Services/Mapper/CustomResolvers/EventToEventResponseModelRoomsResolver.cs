namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.ResponseModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventToEventResponseModelRoomsResolver : IValueResolver<Event, EventResponseModel, ICollection<RoomResponseModel>>
    {
        public ICollection<RoomResponseModel> Resolve(Event source, EventResponseModel destination, ICollection<RoomResponseModel> destMember, ResolutionContext context)
        {
            var roomsResponse = new List<RoomResponseModel>();

            source?.EventRooms?.ToList()
                .ForEach(er => {
                    var roomResponseModel = new RoomResponseModel
                    {
                        Id = er?.Room?.Id.ToString(),
                        Number = er?.Room?.Number
                    };

                    roomsResponse.Add(roomResponseModel);
                });
            
            return roomsResponse;
        }
    }
}
