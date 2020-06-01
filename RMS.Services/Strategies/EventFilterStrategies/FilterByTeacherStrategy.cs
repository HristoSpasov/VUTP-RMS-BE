namespace RMS.Services.Strategies.EventFilterStrategies
{
    using RMS.API.Models.ResponseModels;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FilterByTeacherStrategy : IEventFilterStrategy
    {
        private const string Type = "teacher";

        private readonly IEventService eventService;

        public FilterByTeacherStrategy(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<ICollection<EventResponseModel>> GetFilteredEvents(Guid id)
        {
            return await this.eventService.GetEventsByTeacherIdAsync(id);
        }

        public bool IsApplicable(string type)
        {
            return type.ToLower() == Type;
        }
    }
}
