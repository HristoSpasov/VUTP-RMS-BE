namespace RMS.Services.Contracts
{
    using RMS.API.Models.Helpers;
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        Task<ICollection<EventResponseModel>> GetAllEventsAsync();

        Task<ICollection<EventResponseModel>> GetAllFilteredEventsAsync(EventFilterRequestModel eventFilterRequestModel);

        Task<ValidationStatus> CreateEventAsync(BaseEventRequestModel createEventRequest);

        Task<ValidationStatus> UpdateEventAsync(BaseEventRequestModel updateEventRequest);

        Task<ICollection<EventResponseModel>> GetEventsByRoomIdAsync(Guid roomId, Guid updateEventId = default);

        Task<ICollection<EventResponseModel>> GetEventsByTeacherIdAsync(Guid teacherId, Guid updateEventId = default);

        Task<ICollection<EventResponseModel>> GetEventsByDisciplineIdAsync(Guid disciplineId, Guid updateEventId = default);

        Task<ICollection<EventResponseModel>> GetEventsBySpecialtyIdAsync(Guid specialtyId, Guid updateEventId = default);

        Task DeleteEventAsync(Guid id);
    }
}
