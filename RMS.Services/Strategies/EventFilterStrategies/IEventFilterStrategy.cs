namespace RMS.Services.Strategies.EventFilterStrategies
{
    using RMS.API.Models.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventFilterStrategy
    {
        bool IsApplicable(string type);

        Task<ICollection<EventResponseModel>> GetFilteredEvents(Guid id);
    }
}
