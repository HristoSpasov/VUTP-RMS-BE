namespace RMS.Services.Mapper.CustomResolvers
{
    using AutoMapper;
    using RMS.API.Models.ResponseModels;
    using RMS.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class EventToEventResponseModelTeachersResolver : IValueResolver<Event, EventResponseModel, ICollection<TeacherResponseModel>>
    {
        public ICollection<TeacherResponseModel> Resolve(Event source, EventResponseModel destination, ICollection<TeacherResponseModel> destMember, ResolutionContext context)
        {
            var teachersResponse = new List<TeacherResponseModel>();

            source?.EventTeachers?.ToList()
                .ForEach(et => {
                    var teacherResponseModel = new TeacherResponseModel
                    {
                        Id = et?.Teacher?.Id.ToString(),
                        AcademicTitle = et?.Teacher?.AcademicTitle,
                        FirstName = et?.Teacher?.FirstName,
                        LastName = et?.Teacher?.LastName
                    };

                    teachersResponse.Add(teacherResponseModel);
                });

            return teachersResponse;
        }
    }
}
