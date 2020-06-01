namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.ResponseModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventToEventResponseModelDisciplinesResolver : IValueResolver<Event, EventResponseModel, ICollection<DisciplineResponseModel>>
    {
        public ICollection<DisciplineResponseModel> Resolve(Event source, EventResponseModel destination, ICollection<DisciplineResponseModel> destMember, ResolutionContext context)
        {
            var disciplines = new List<DisciplineResponseModel>();

            source?.EventDisciplines?.ToList().ForEach(ed => 
            {
                var disciplineResponse = new DisciplineResponseModel
                {
                    Id = ed?.Discipline?.Id.ToString(),
                    Name = ed?.Discipline?.Name
                };

                disciplines.Add(disciplineResponse);
            });

            return disciplines;
        }
    }
}
