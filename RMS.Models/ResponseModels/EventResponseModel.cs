namespace RMS.API.Models.ResponseModels
{
    using System;
    using System.Collections.Generic;

    public class EventResponseModel
    {
        public Guid Id { get; set; }

        public ICollection<DisciplineResponseModel> Disciplines { get; set; }

        public ICollection<TeacherResponseModel> Teachers { get; set; }

        public ICollection<RoomResponseModel> Rooms { get; set; }

        public ICollection<SpecialtyResponseModel> Specialties { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
