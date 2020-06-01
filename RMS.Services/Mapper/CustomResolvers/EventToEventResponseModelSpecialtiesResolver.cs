namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.ResponseModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventToEventResponseModelSpecialtiesResolver : IValueResolver<Event, EventResponseModel, ICollection<SpecialtyResponseModel>>
    {
        public ICollection<SpecialtyResponseModel> Resolve(Event source, EventResponseModel destination, ICollection<SpecialtyResponseModel> destMember, ResolutionContext context)
        {
            var specialtiesReponse = new List<SpecialtyResponseModel>();

            source?.EventSpecialties?.ToList()
                .ForEach(es =>
                {
                    var specialtyResponse = new SpecialtyResponseModel
                    {
                        Id = es?.Specialty?.Id.ToString(),
                        Grade = es.Specialty != null ? es.Specialty.Grade : 0,
                        Name = es?.Specialty?.Name
                    };

                    specialtiesReponse.Add(specialtyResponse);
                });

            return specialtiesReponse;
        }
    }
}
