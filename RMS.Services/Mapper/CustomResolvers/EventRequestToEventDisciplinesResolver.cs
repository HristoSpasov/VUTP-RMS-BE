namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRequestToEventDisciplinesResolver : IValueResolver<BaseEventRequestModel, Event, ICollection<DisciplineEvent>>
    {
        public ICollection<DisciplineEvent> Resolve(BaseEventRequestModel source, Event destination, ICollection<DisciplineEvent> destMember, ResolutionContext context)
        {
            var eventDisciplines = new List<DisciplineEvent>();

            source?.Disciplines?.ToList()
                .ForEach(dId =>
                {
                    eventDisciplines.Add(new DisciplineEvent { DisciplineId = dId, Event = destination });
                });

            return eventDisciplines;
        }
    }
}
