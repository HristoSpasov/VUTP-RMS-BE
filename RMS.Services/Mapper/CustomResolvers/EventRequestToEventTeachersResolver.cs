namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRequestToEventTeachersResolver : IValueResolver<BaseEventRequestModel, Event, ICollection<TeacherEvent>>
    {
        public ICollection<TeacherEvent> Resolve(BaseEventRequestModel source, Event destination, ICollection<TeacherEvent> destMember, ResolutionContext context)
        {
            var eventTeachers = new List<TeacherEvent>();

            source?.Teachers?.ToList()
                .ForEach(tId =>
                {
                    eventTeachers.Add(new TeacherEvent { TeacherId = tId, Event = destination });
                });

            return eventTeachers;
        }
    }
}
