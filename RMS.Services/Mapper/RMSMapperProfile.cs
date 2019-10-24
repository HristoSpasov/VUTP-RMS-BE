namespace RMS.Services.Mapper
{
    using API.Models.ResponseModels;
    using AutoMapper;
    using Data.Entities;

    /// <summary>
    /// Auto mapper profile configuration.
    /// </summary>
    public class RMSMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RMSMapperProfile"/> class. 
        /// </summary>
        public RMSMapperProfile()
        {
            this.CreateMap<Teacher, TeacherResponseModel>().ReverseMap();
        }
    }
}