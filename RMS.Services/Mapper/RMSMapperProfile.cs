namespace RMS.Services.Mapper
{
    using API.Models.ResponseModels;
    using AutoMapper;
    using Data.Entities;

    public class RMSMapperProfile : Profile
    {
        public RMSMapperProfile()
        {
            this.CreateMap<Room, RoomResponseModel>();
            this.CreateMap<Teacher, TeacherResponseModel>();
            this.CreateMap<Specialty, SpecialtyResponseModel>();
        }
    }
}