namespace RMS.Services.Mapper
{
    using API.Models.ResponseModels;
    using AutoMapper;
    using Data.Entities;
    using RMS.API.Models.RequestModels;
    using RMS.Services.Mapper.CustomResolvers;

    /// <summary>
    /// Auto mapper profile configuration.
    /// </summary>
    public class RMSMapperProfile : Profile
    {
        public RMSMapperProfile()
        {
            this.CreateMap<Room, RoomResponseModel>();
            this.CreateMap<Teacher, TeacherResponseModel>();
            this.CreateMap<Discipline, DisciplineResponseModel>();
            this.CreateMap<Specialty, SpecialtyResponseModel>();
            this.CreateMap<Event, EventResponseModel>()
                .ForMember(dest => dest.Disciplines, opts => opts.ResolveUsing<EventToEventResponseModelDisciplinesResolver>())
                .ForMember(dest => dest.Teachers, opts => opts.ResolveUsing<EventToEventResponseModelTeachersResolver>())
                .ForMember(dest => dest.Specialties, opts => opts.ResolveUsing<EventToEventResponseModelSpecialtiesResolver>())
                .ForMember(dest => dest.Rooms, opts => opts.ResolveUsing<EventToEventResponseModelRoomsResolver>());

            this.CreateMap<CreateTeacherRequestModel, Teacher>();
            this.CreateMap<UpdateTeacherRequestModel, Teacher>();
            this.CreateMap<CreateDisciplineRequestModel, Discipline>();
            this.CreateMap<UpdateDisciplineRequestModel, Discipline>();
            this.CreateMap<CreateRoomRequestModel, Room>();
            this.CreateMap<UpdateRoomRequestModel, Room>();
            this.CreateMap<CreateSpecialtyRequestModel, Specialty>();
            this.CreateMap<UpdateSpecialtyRequestModel, Specialty>();
            this.CreateMap<RegisterUserRequstModel, User>();
            this.CreateMap<BaseEventRequestModel, BaseEventRequestModel>();
            this.CreateMap<BaseEventRequestModel, Event>()
                .ForMember(dest => dest.StartTime, opts => opts.MapFrom(s => s.StartTime))
                .ForMember(dest => dest.EndTime, opts => opts.MapFrom(s => s.EndTime))
                .ForMember(dest => dest.EventDisciplines, opts => opts.ResolveUsing<EventRequestToEventDisciplinesResolver>())
                .ForMember(dest => dest.EventTeachers, opts => opts.ResolveUsing<EventRequestToEventTeachersResolver>())
                .ForMember(dest => dest.EventSpecialties, opts => opts.ResolveUsing<EventRequestToEventSpecialtiesResolver>())
                .ForMember(dest => dest.EventRooms, opts => opts.ResolveUsing<EventRequestToEventRoomsResolver>());
        }
    }
}