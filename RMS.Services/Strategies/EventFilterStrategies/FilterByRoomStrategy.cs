namespace RMS.Services.Strategies.EventFilterStrategies
{
    using RMS.API.Models.ResponseModels;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FilterByRoomStrategy : IEventFilterStrategy
    {
        private const string Type = "room";

        private readonly IEventService eventService;

        public FilterByRoomStrategy(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<ICollection<EventResponseModel>> GetFilteredEvents(Guid id)
        {
            return await this.eventService.GetEventsByRoomIdAsync(id);
        }

        public bool IsApplicable(string type)
        {
            return type.ToLower() == Type;
        }
    }
}
