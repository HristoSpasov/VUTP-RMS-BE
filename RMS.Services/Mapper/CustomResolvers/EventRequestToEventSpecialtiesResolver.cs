namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRequestToEventSpecialtiesResolver : IValueResolver<BaseEventRequestModel, Event, ICollection<SpecialtyEvent>>
    {
        public ICollection<SpecialtyEvent> Resolve(BaseEventRequestModel source, Event destination, ICollection<SpecialtyEvent> destMember, ResolutionContext context)
        {
            var eventSpecialties = new List<SpecialtyEvent>();

            source?.Specialties?.ToList()
                .ForEach(sId =>
                {
                    eventSpecialties.Add(new SpecialtyEvent { SpecialtyId = sId, Event = destination });
                });

            return eventSpecialties;
        }
    }
}
